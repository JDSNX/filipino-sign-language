import cv2
import numpy as np
import os
import sys
    
os.environ['TF_CPP_MIN_LOG_LEVEL'] = '3'

from module import Module

class SignLanguage(Module):

    def __init__(self, video_channel=None, threshold=0.5):
        super().__init__()
        self.init()
        self.video_channel = video_channel
        self.actions = np.array(self.get_keywords())
        self.translation = self.get_translation()
        self.threshold = threshold

    def new_word(self):
        cap = cv2.VideoCapture(self.video_channel)

        with self.mp_holistic.Holistic(min_detection_confidence=0.5, min_tracking_confidence=0.5) as holistic:
            tmp_actions = [action for action in self.actions if self.get_size(action)]

            if len(tmp_actions) == 0:
                print('No new words')
   
            for action in tmp_actions:
                if not action.strip() == '':
                    for video in range(self.no_of_videos):
                        for frame_num in range(self.frames_of_video):
                            ret, frame = cap.read()
                            
                            if not ret:
                                break

                            image, results = self.mediapipe_detection(frame, holistic)
                            self.draw_landmarks(image, results)

                            if frame_num == 0: 
                                cv2.putText(image, 'Collecting...', (120,200), 
                                        cv2.FONT_HERSHEY_SIMPLEX, 1, (0,255, 0), 3, cv2.LINE_AA)
                                cv2.putText(image, '{} | Video No.: {}'.format(action, video), (15,12), 
                                        cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 0, 255), 1, cv2.LINE_AA)

                                cv2.imshow('Capture new words', image)
                                cv2.waitKey(1000)
                            else: 
                                cv2.putText(image, '{} | Video No.: {}'.format(action, video), (15,12), 
                                        cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 0, 255), 1, cv2.LINE_AA)
                                cv2.imshow('Capture new words', image)

                            keypoints = self.extract_keypoints(results)
                            npy_path = os.path.join(self.DATA_PATH, action, str(video), str(frame_num))
                            np.save(npy_path, keypoints)

                            if cv2.waitKey(10) & 0xFF == ord('q'):
                                break

        cap.release()
        cv2.destroyAllWindows()

    def preprocess_data(self):
        from sklearn.model_selection import train_test_split
        from keras.utils import to_categorical
        from tensorflow.python.keras.models import Sequential
        from tensorflow.python.keras.callbacks import TensorBoard
        from tensorflow.python.keras.layers import LSTM, Dense

        label_map = {label:num for num, label in enumerate(self.actions)}
        log_dir = os.path.join('Logs')
        tb_callback = TensorBoard(log_dir=log_dir)

        sequences, labels = [], []
        for action in self.actions:
            if not action.strip() == '':
                for sequence in np.array(os.listdir(os.path.join(self.DATA_PATH, action))).astype(int):
                    window = []
                    for frame_num in range(self.frames_of_video):
                        res = np.load(os.path.join(self.DATA_PATH, action, str(sequence), "{}.npy".format(frame_num)))
                        window.append(res)
                    sequences.append(window)
                    labels.append(label_map[action])

        X = np.array(sequences)
        y = to_categorical(labels).astype(int)

        X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.05)

        # Build network
        model = Sequential()
        model.add(LSTM(64, return_sequences=True, activation='relu', input_shape=(30,1662)))
        model.add(LSTM(128, return_sequences=True, activation='relu'))
        model.add(LSTM(64, return_sequences=False, activation='relu'))
        model.add(Dense(64, activation='relu'))
        model.add(Dense(32, activation='relu'))
        model.add(Dense(self.actions.shape[0], activation='softmax'))
        model.compile(optimizer='Adam', loss='categorical_crossentropy', metrics=['categorical_accuracy'])
        model.fit(X_train, y_train, epochs=2000, callbacks=[tb_callback])
        model.save(self.MODEL)   

    def main(self):
        from keras.models import load_model
        
        cap = cv2.VideoCapture(self.video_channel)
        model = load_model(self.MODEL)
        sequence = []
        sentence = []
        predictions = []

        with self.mp_holistic.Holistic(min_detection_confidence=0.5, min_tracking_confidence=0.5) as holistic:
            while cap.isOpened():
                ret, frame = cap.read()

                if not ret:
                    break

                image, results = self.mediapipe_detection(frame, holistic)
                            
                keypoints = self.extract_keypoints(results)
                sequence.append(keypoints)
                sequence = sequence[-30:]

                if len(sequence) == 30:
                    res = model.predict(np.expand_dims(sequence, axis=0), verbose=0)[0]
                    s = predictions.append(np.argmax(res))
                    
                    if np.unique(predictions[-10:])[0]==np.argmax(res): 
                        if res[np.argmax(res)] > self.threshold: 
                            
                            if len(sentence) > 0: 
                                if self.actions[np.argmax(res)] != sentence[-1]:
                                    sentence.append(self.actions[np.argmax(res)])
                            else:
                                sentence.append(self.actions[np.argmax(res)])

                    if len(sentence) > 5: 
                        sentence = sentence[-1]

                last = ""
                if len(sentence) > 0:
                    last = sentence.pop()
                    last = self.translation[last]
                
                cv2.putText(image, last, (3,30), cv2.FONT_HERSHEY_SIMPLEX, 2, (0, 0, 255), 2, cv2.LINE_AA)
            
                cv2.imshow('Press Q to exit FSL', image)

                if cv2.waitKey(10) & 0xFF == ord('q'):
                    print(cv2.getWindowProperty('Press Q to exit FSL', cv2.WND_PROP_VISIBLE))
                    break

            cap.release()
            cv2.destroyAllWindows()
        

if __name__ == '__main__':
    args = sys.argv[1]

    fsl = SignLanguage(0)

    if args == 'run':
        fsl.main()

    elif args == 'train':
        fsl.preprocess_data()

    elif args == 'new_word':
        fsl.new_word()

    else:
        print('args list: run, train, new_word')

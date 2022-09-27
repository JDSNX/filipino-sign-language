import requests
import cv2
import os
import numpy as np
import xml.etree.ElementTree as et
import mediapipe as mp

from pathlib import Path
from dotenv import load_dotenv

class Module:
    load_dotenv()

    def __init__(self):
        self.DATA_PATH='mediapipe_data'
        self.MODEL='model.h5'
        self.KW_PATH = f"C:\\Users\\judis\\AppData\\Local\\FSL\\FSL_Url_gx1zzqp5v2mrfnm5flx2k1sy5tbzkc0b\\1.0.0.0\\user.config"
        self.no_of_videos = 0
        self.frames_of_video = 0

        self.mp_drawing = mp.solutions.drawing_utils
        self.mp_holistic = mp.solutions.holistic

        # user = os.environ.get("username")
        # response = requests.get(os.getenv('GET_USER') + f'={user}')
        
        # try:
        #     response.raise_for_status()
        # except requests.exceptions.HTTPError:
        #     print("-- HTTP ERROR --")
        # else:
        #     if not os.path.exists(response):
        #         response = response.replace('Roaming', 'Local')

        root = et.parse(self.KW_PATH).getroot()

        for item in root.iter('setting'):
            cfg = item.items()[0][1]
            for label in item.iter("value"):
                self.KW_PATH = label.text if cfg == "KW_PATH" else self.KW_PATH
                self.no_of_videos = int(label.text) if cfg == "NO_OF_VIDEOS" else self.no_of_videos
                self.frames_of_video = int(label.text) if cfg == "FRAME_OF_VIDEOS" else self.frames_of_video

    def mediapipe_detection(self, image, model):
        image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB) 
        image.flags.writeable = False
        results = model.process(image)
        image.flags.writeable = True
        image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)
        return image, results

    def get_size(self, action):
        size = 0
        for path, dirs, files in os.walk(f'./{self.DATA_PATH}/{action}'.strip()):
            for f in files:
                fp = os.path.join(path, f)
                size += os.path.getsize(fp)
        return True if int(size) == 0 else False

    def draw_landmarks(self, image, results):
        self.mp_drawing.draw_landmarks(image, results.right_hand_landmarks, self.mp_holistic.HAND_CONNECTIONS)
        self.mp_drawing.draw_landmarks(image, results.left_hand_landmarks, self.mp_holistic.HAND_CONNECTIONS)
        self.mp_drawing.draw_landmarks(image, results.pose_landmarks, self.mp_holistic.POSE_CONNECTIONS)

    def extract_keypoints(self, results):
        pose = np.array([[res.x, res.y, res.z, res.visibility] for res in results.pose_landmarks.landmark]).flatten() if results.pose_landmarks else np.zeros(132)
        face = np.array([[res.x, res.y, res.z] for res in results.face_landmarks.landmark]).flatten() if results.face_landmarks else np.zeros(468*3)
        lh = np.array([[res.x, res.y, res.z] for res in results.left_hand_landmarks.landmark]).flatten() if results.left_hand_landmarks else np.zeros(63)
        rh = np.array([[res.x, res.y, res.z] for res in results.right_hand_landmarks.landmark]).flatten() if results.right_hand_landmarks else np.zeros(63)
        return np.concatenate([pose, face, lh, rh])

    def get_keywords(self):
        self.create_csv()
        with open(self.KW_PATH) as f:
            return np.array([line.split(',')[0] for line in f])

    def get_translation(self):
        data = {}
        with open(self.KW_PATH) as f:
            for rows in f:
                s = rows.strip().split(',')
                data[s[0]] = s[1]
        return data

    def create_csv(self):
        if not os.path.exists(self.KW_PATH):
            fle = Path(self.KW_PATH)
            fle.touch(exist_ok=True)
            f = open(fle)
            return False
        return True

    def create_mp_data(self):
        for action in self.get_keywords(): 
            for video in range(self.no_of_videos):
                try:
                    os.makedirs(os.path.join(self.DATA_PATH, action, str(video)))
                except:
                    pass

    def init(self):
        self.create_csv()
        self.create_mp_data()

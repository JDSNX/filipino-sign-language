import mediapipe as mp
import cv2
import os
import numpy as np
import csv
import xml.etree.ElementTree as et
import requests

from pathlib import Path
from tensorflow.python.keras.layers import LSTM, Dense

mp_drawing = mp.solutions.drawing_utils
mp_holistic = mp.solutions.holistic

user = os.environ.get("username")
response = requests.get(f'https://fsl-config.herokuapp.com/get?username={user}').json()
DATA_PATH = os.path.join('mediapipe_data')
KW_PATH = ""
no_of_videos = 0
frames_of_video = 0
MODEL = 'model.h5'

if not os.path.exists(response):
    response = response.replace('Roaming', 'Local')

tree = et.parse(response)
root = tree.getroot()

for item in root.iter('setting'):
    cfg = item.items()[0][1]
    for label in item.iter("value"):
        KW_PATH = label.text if cfg == "KW_PATH" else KW_PATH
        no_of_videos = int(label.text) if cfg == "NO_OF_VIDEOS" else no_of_videos
        frames_of_video = int(label.text) if cfg == "FRAME_OF_VIDEOS" else frames_of_video

def mediapipe_detection(image, model):
    image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB) 
    image.flags.writeable = False
    results = model.process(image)
    image.flags.writeable = True
    image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)
    return image, results

def get_size(action):
    size = 0
    for path, dirs, files in os.walk(os.path.join(DATA_PATH, action)):
        for f in files:
            fp = os.path.join(path, f)
            size += os.path.getsize(fp)
    return size

def draw_landmarks(image, results):
    mp_drawing.draw_landmarks(image, results.right_hand_landmarks, mp_holistic.HAND_CONNECTIONS)
    mp_drawing.draw_landmarks(image, results.left_hand_landmarks, mp_holistic.HAND_CONNECTIONS)
    mp_drawing.draw_landmarks(image, results.pose_landmarks, mp_holistic.POSE_CONNECTIONS)

def extract_keypoints(results):
    pose = np.array([[res.x, res.y, res.z, res.visibility] for res in results.pose_landmarks.landmark]).flatten() if results.pose_landmarks else np.zeros(132)
    face = np.array([[res.x, res.y, res.z] for res in results.face_landmarks.landmark]).flatten() if results.face_landmarks else np.zeros(468*3)
    lh = np.array([[res.x, res.y, res.z] for res in results.left_hand_landmarks.landmark]).flatten() if results.left_hand_landmarks else np.zeros(63)
    rh = np.array([[res.x, res.y, res.z] for res in results.right_hand_landmarks.landmark]).flatten() if results.right_hand_landmarks else np.zeros(63)
    return np.concatenate([pose, face, lh, rh])

def get_keywords():
    create_csv()
    with open(KW_PATH) as f:
        return np.array([line.split(',')[0] for line in f])

def get_translation():
    data = {}
    with open(KW_PATH) as f:
        for rows in f:
            s = rows.split(',')
            data[s[0].strip()] = s[1].strip()
    return data

def create_csv():
    if not os.path.exists(KW_PATH):
        fle = Path(KW_PATH)
        fle.touch(exist_ok=True)
        f = open(fle)
        return False
    return True

def create_mp_data():
    for action in get_keywords(): 
        for video in range(no_of_videos):
            try:
                os.makedirs(os.path.join(DATA_PATH, action, str(video)))
            except:
                pass

def init():
    create_csv()
    create_mp_data()
    
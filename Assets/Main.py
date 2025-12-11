from cvzone.HandTrackingModule import HandDetector
import cv2
import socket

cap = cv2.VideoCapture(0)

cap.set(3, 1920)
cap.set(4, 1080)

success, img = cap.read()
if not success:
    print("Camera error")
    exit()

img = cv2.flip(img, 1)
h, w, _ = img.shape
detector = HandDetector(detectionCon=0.8, maxHands=2)
PHONE_IP = "192.168.0.109"   # enter phone IP
LAPTOP_IP = "192.168.0.101"  # enter laptop IP on the same WiFi
PORT = 5052


sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)


while True:
    success, img = cap.read()
    img = cv2.flip(img, 1)

    hands, img = detector.findHands(img)
    L = []
    R = []

    if hands:
        for hand in hands:
            lmList = hand["lmList"]
            handType = hand["type"]

            arr = []
            for (id, lm) in enumerate(lmList):
                arr.extend([lm[0], h - lm[1], lm[2]])

            if handType == "Left":
                L = arr
            else:
                R = arr

    packet = f"[L:{','.join(map(str, L))}|R:{','.join(map(str, R))}]"
    sock.sendto(packet.encode(), (PHONE_IP, PORT))
    sock.sendto(packet.encode(), (LAPTOP_IP, PORT))

    cv2.imshow("Image", img)
    cv2.waitKey(1)

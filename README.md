# VR Playground Handmapping: Vision–IMU Based Interactive Virtual Environment for Children

This project presents a tilt-based virtual reality playground that integrates computer-vision-based hand tracking, gesture recognition, and real-time data transmission to Unity applications. The system operates without Google Cardboard or traditional VR controllers. Instead, it relies on MediaPipe for hand landmark extraction and Unity for gesture-driven interaction.

---

## 1. Overview

The system enables users to interact with virtual objects through natural hand movements. A Python module performs hand detection and gesture analysis, while Unity receives and visualises the hand positions. The setup supports both **desktop (laptop Unity)** and **Android Unity builds**, allowing parallel real-time input from a single MediaPipe source.

---

## 2. Hand Tracking Pipeline (Python – MediaPipe)

The hand tracking component uses MediaPipe Hands to detect **21 anatomical landmarks** for both the left and right hand. The pipeline includes:

1. Webcam frame acquisition.
2. Landmark detection and normalisation.
3. Gesture classification, including:

   * Pinch (thumb–index distance threshold)
   * Open palm (finger-extension heuristic)
   * Swipe (velocity-based directional analysis)
4. Encoding of coordinates and gesture flags into a compact UDP packet.
5. Transmission of the data to Unity clients.

---

## 3. Unity Integration

Unity operates as the receiving and interaction layer. The system supports two-hand mapping and gesture-based object manipulation.

### 3.1 Two-Hand Mapping

* Unity receives UDP packets containing 21 × 2 landmark positions.
* The coordinates are mapped to 3D models representing the user’s hands.
* The project uses tilt-based rotation (gyro integration may be added later).

### 3.2 Gesture-Driven Interactions

The following interactions are supported:

#### Pinch — Object Selection or Grabbing

* A raycast originates from the palm direction.
* If the ray intersects with an interactive object within a predefined distance:

  * The object is parented to the hand’s transform.
  * Physics may be temporarily disabled using `rb.isKinematic = true`.

#### Open Palm — Object Release

* Removes the parent–child relationship.
* Restores physics properties.

#### Swipe — Object Pushing

* Direction is estimated from hand velocity.
* The object struck by the raycast receives a force in the swipe direction.

---

## 4. Data Transmission Architecture (Dual Output)

The system supports simultaneous output to two receivers.

### 4.1 Architecture

```
Laptop Camera → Python MediaPipe → UDP → Laptop Unity
                                     → UDP → Android Phone Unity
```

A single MediaPipe instance distributes identical hand-tracking data to both the desktop and the phone.

### 4.2 Python Implementation

To achieve dual transmission, the Python script sends the same UDP packet to two IP addresses:

```python
PHONE_IP = "192.168.x.x"   # Android device IP
LAPTOP_IP = "192.168.x.x"  # Laptop IP (same network)
PORT = 5052

# Dual-output transmission
sock.sendto(message, (PHONE_IP, PORT))
sock.sendto(message, (LAPTOP_IP, PORT))
```

Ensure that both devices are connected to the same Wi-Fi network.

Laptop IP can be retrieved using:

```
ipconfig → IPv4 Address
```

---

## 5. APK Distribution

A prebuilt Android package is available in the repository:

**Repository Path:** `Builds/Build.apk`

The APK can be installed on any Android device. After installation, update the `main.py` IP settings according to the device and laptop used.

This enables direct deployment without rebuilding the Unity project.

---

## 6. System Flow Summary

1. Laptop camera captures hand images.
2. Python MediaPipe performs landmark detection.
3. Gesture classification is applied.
4. Dual-output UDP packets are transmitted.
5. Unity (desktop and Android) receives and reconstructs hand poses.
6. Unity applies gesture rules for object interaction.

---

## 👥 Contributors

- **Hyunjae Gil** — Project Mentor 
- **Sharvari Kamble** — ML Integrator, Unity Development



https://github.com/user-attachments/assets/92b4a8af-193b-43d8-83e7-20376a310322



## 📎 Repository Roadmap Files
- `README.md` – Project documentation  
- `asset/` – c# and python files 



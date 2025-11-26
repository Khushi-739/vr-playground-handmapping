# VR Playground Handmapping: Vision–IMU Based Interactive Virtual Environment for Children

## 📌 Project Overview
This project aims to build a lightweight **VR playground experience for children**, using a combination of **smartphone-based VR (Google Cardboard)**, **IMU sensor data**, and **basic hand-tracking using phone camera vision**.  
The user can move their head (IMU rotation), interact with objects in a 3D virtual playground, and see simple responses such as **collisions, sound effects, and object reactions**.  
The system captures a child’s hand in front of the mobile camera, estimates its position using a vision model, maps it into the Unity VR scene, and enables natural interaction with virtual objects.

---

## 🎯 Project Objectives

- Build a **Unity-based VR playground** optimized for smartphones (Google Cardboard).
- Integrate **IMU sensor data** to control camera orientation and movement.
- Implement **real-time hand capture** using the phone camera and a simple vision model (e.g., MediaPipe or custom CV).
- Map detected hand coordinates to a **3D virtual hand** inside the VR scene.
- Enable **interaction with virtual objects** through collisions, sounds, and simple animations.
  
---

## 🛠 Tech Stack

### Core
- **Unity 3D**
- **C# (Unity scripting)**
- **Google Cardboard XR Plugin / Unity XR Management**
- **Android Build Support**

### Computer Vision
- **MediaPipe Hands** (preferred)  
  or  
- Basic **OpenCV-based hand detection** (Unity WebcamTexture integration)

### Sensors
- Smartphone **IMU (accelerometer + gyroscope)**  
  (Unity Input System / Device Sensors API)

### Version Control
- Git + GitHub repository (shared before Dec 12)

---

## 🚀 How to Run the Project

### 1. Clone Repository
```bash
git clone https://github.com/Khushi-739/vr-playground-handmapping
cd vr-playground
```

### 2. Open in Unity
- Install Unity **2021 LTS or higher**
- Open project folder
- Verify Google Cardboard / XR plugins enabled

### 3. Build for Android
- Enable **XR Plugin Management → Cardboard**
- Switch platform to **Android**
- Build APK
- Install APK on Android device
- Insert phone into Google Cardboard viewer

### 4. Using the Demo
- Launch the app  
- Move your head → VR camera rotates (IMU-based)  
- Place hand in front of camera → virtual hand appears  
- Touch playground objects → collision triggers reactions & sounds  

---

## 👥 Contributors

- **Hyunjae Gil** — Project Mentor 
- **Sharvari Kamble** — ML Integrator, Unity Development, Vision–IMU Interaction Mapping  

---

## 📅 Milestones (High-Level)
1. **Unity Playground Scene Setup**  
   – Basic environment, VR camera rig, object prefabs  
2. **IMU Integration**  
   – Map smartphone rotation + acceleration to VR camera  
3. **Vision Module**  
   – Hand detection → coordinate extraction → smoothing  
4. **Hand-to-VR Mapping**  
   – Visual hand model inside Unity  
5. **Interaction System**  
   – Collisions, sound triggers, basic physics feedback  
6. **Poster Demo Build**  
   – Stable APK + poster figures + video recording  

---

## 📎 Repository Roadmap Files
- `README.md` – Project documentation  
- `docs/` – Meeting notes, diagrams (to be added)  



# AOOP Project

A real-time multiplayer 2D adventure game and backend system built for the Advanced Object-Oriented Programming (AOOP) course.

This repository contains both the Unity game client and the Spring Boot Java backend that powers the multiplayer mechanics, authentication, and game state persistence.

## 🎮 Game Features (Unity Client)
* **Real-time Multiplayer:** Explore and fight alongside other players in the same game rooms.
* **Dynamic Cutscenes:** High-quality video cutscenes and pixel-art backgrounds.
* **Combat & Traps:** Face off against enemies (crabs, octopuses) and avoid dangerous traps (saws, spike balls, mud boxes).
* **Parallax Environments:** Beautiful multi-layered environments with dynamic weather (clouds, night sky) and sound effects.

## ⚙️ Backend Architecture (Spring Boot)
* **WebSocket Communication:** Handles real-time positional updates and game events between players.
* **REST API & Authentication:** Secure user registration, login, and session management.
* **Persistence:** Saves player progress, health, and high scores securely in an H2 database.
* **Multi-Mode Support:** Handles isolated game rooms to ensure players only interact with others in their specific session.

## 🚀 Tech Stack
* **Frontend/Client:** Unity (C#)
* **Backend:** Java 17, Spring Boot, Spring Security, Spring WebSockets, H2 Database
* **Build Tool (Backend):** Maven
* **Version Control:** Git & Git LFS (Large File Storage)

## 🛠️ How to Run

### Backend
1. Navigate to the `AOOP-Backend` folder.
2. Run the application using Maven: `./mvnw spring-boot:run`
3. The server will start on `localhost:8080`.

### Unity Game
1. Open Unity Hub and add the `AOOP` folder as a project.
2. Open the `Main Menu` scene located in `Assets/Scenes`.
3. Press Play in the Unity Editor to start the game and connect to the local backend.

<img width="978" height="122" alt="download" src="https://github.com/user-attachments/assets/89033e16-e07e-4ea3-ad97-a6414e4ec4fa" />


>   A digital sanctuary for  modern students, built to turn on chaotic schedules into structured, aesthetic, and actionable  success.

<div align="center">

**_Ready To Optimize Your Semester With Us?_**

</div>

---

### 📌System Overview

***Acadeño*** is an centralized desktop application built to improve student productivity and workflow through automatic task prioritization and simulating grade outcomes; a priority-based management system.

Recognizing the overwhelming nature of modern education, ***Acadeño*** implements a priority-based management system that removes the guesswork from scheduling. Unlike standard digital planners, this system features a predictive logic engine designed to help students manage their time effectively and maintain a data-driven perspective on their academic performance.

---

### UML
<img width="6488" height="9441" alt="Deliverables - UML (6)" src="https://github.com/user-attachments/assets/914fce4e-1072-4872-86b6-b9dccb0a6fe3" />


---

## ✒️ Key Features and Functionalities

### 🖥️ Dashboard
A centralized analytics hub featuring:
* A digital clock and calendar integration.
* Instant visibility of your current ***GWA/GPA*** and total units loaded.
* An automated _Risk Level_ system to flag urgent academic tasks.

### 📚 Course Management & Timetable
* Dynamic interfaces to manage subjects, schedules, and room locations, alongside integrated tracking for homework, activities, and exams.
* A synchronized weekly timetable to visualize lecture and laboratory sessions.
* Real-time tracking of individual subject performance.

### 🧮 Grade Simulator
"What-If" analysis tool:
* Input hypothetical scores for quizzes and exams to calculate final grade outcomes.
* Monitor your Academic Standing (e.g., *Dean's Lister*) through the built-in weighted mean calculator

### ⚙️ Priority Engine
* Automatically ranks tasks by combining deadline urgency with assignment value.
---

### How the program works?
Acadeño is a Blazor MAUI Hybrid App. It uses C# as the "engine" and web technology as the "skin," compiled into a native Windows application.

## 🏗️ The Architecture
* **Logic Components (.razor):** UI "Skeletons" that house both HTML and C# logic.
* **CSS Isolation (.razor.css):** Isolated CSS that prevents design leaks across pages.
* **The Backend (.cs):** Pure logic files. They handle the math, the database, and the thinking.

## 🧠 Core VIP Files
* **AppDbContext:** The bridge to SQLite; handles all data persistence.
* **MainLayout:** The UI Foundation; houses the persistent sidebar and navigation frame.
* **Routes:** The Traffic Cop; manages navigation and "Remember Me" session logic.
* **MauiProgram:** The Blueprint where the app is "wired" together via Dependency Injection.
---

### How to run the application?
1. Clone the Repository
   ```
   git clone https://github.com/ennage/acadeno.git
   ```
2. Build and Compile
   ```
   dotnet restore
   dotnet build
3. Run
   ```
   dotnet run -f net8.0-windows10.0.19041.0
---

### Members
```
Project Manager: Calabia, Geanne Margarete M.
GUI: Buenviaje, Lance T.
Logic Developer/Tester: Aguilar, Azelle Ann C.

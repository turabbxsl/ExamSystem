# Exam System

Exam System is a backend-focused system built on .NET 10, designed to manage exams, courses, and student records. The project follows Clean Architecture principles, ensuring a clear separation of concerns, high maintainability, and testability.

## Technology Stack

* **Backend:** .NET 10, ASP.NET Core Web API, MVC
* **Frontend:** Vue.js, Axios
* **Database:** SQL Server
* **Infrastructure:** Docker, Docker Compose
* **Data Access:** Entity Framework Core (with Unit of Work and Generic Repository patterns)

## Key Architectural Features

* **Transaction Management:** Centralized transaction handling using a `BaseService` wrapper and `UnitOfWork` pattern to ensure atomic operations and data integrity.
* **Result Pattern:** Avoids throwing exceptions for control flow by returning a standard `Result<T>` object for success/failure states.
* **Clean Architecture:** Strict separation between Core, Application, and Infrastructure layers.

## Quick Start

Ensure you have Docker installed. To build and run the entire environment (API, Web, and SQL Server containers), execute the following command in the project root:

```bash
docker compose up --build
```

Once the services are active, you can access the application via your browser:

Dashboard URL: 
```bash
http://localhost:5246/dashboard
```
<img width="1892" height="943" alt="Screenshot 2026-04-25 172611" src="https://github.com/user-attachments/assets/f7ea0d16-fb78-49bb-81c0-55e9fff60f3b" />


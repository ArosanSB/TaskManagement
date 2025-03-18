# Task Management API 🚀
[![CI/CD Pipeline](https://github.com/ArosanSB/TaskManagement/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/ArosanSB/TaskManagement/actions/workflows/dotnet-desktop.yml)
## About This Project
This project was built for **educational purposes** 📚 because there were no active projects at work.  
I decided to **educate myself** by implementing various **services, architectures, and design patterns**  
to enhance my skills in **.NET, Clean Architecture, Docker, and CI/CD.**  

This repository is a **Task Management API**, developed with **.NET 8, PostgreSQL, and Angular**.

---

## **🛠 Technologies Used**
This project follows best practices and modern software architecture, including:

- **.NET 8** - Backend API
- **Entity Framework Core** - Database ORM
- **PostgreSQL** - Database
- **Docker & Docker Compose** - Containerized setup
- **MediatR (CQRS)** - Separating business logic from handlers
- **AutoMapper** - Mapping DTOs to domain entities
- **Serilog** - Logging for debugging and monitoring
- **JWT Authentication** - Secure authentication & authorization (Not added yet)
- **Unit Testing with xUnit** - Testing API endpoints and business logic (Not added yet) 
- **GitHub Actions (CI/CD)** - Automating builds, tests, and deployment 
- **Angular** - Frontend application (work in progress)

---

## 📂 Project Structure

This project follows **Clean Architecture** to ensure modularity and scalability.

### 🏗️ What is Clean Architecture?

Clean Architecture is a software design pattern that promotes **separation of concerns**, ensuring that each layer of the application has a distinct responsibility. This leads to **maintainability, testability, and scalability** of the codebase.

#### 🔹 Key Principles of Clean Architecture:

- **Independence of Frameworks**: The core logic does not depend on external libraries or frameworks.
- **Testability**: Business rules can be tested independently from UI, database, and external services.
- **Separation of Concerns**: Dividing the application into layers such as **Presentation, Application, Domain, and Infrastructure**.
- **Dependency Rule**: Inner layers should not depend on outer layers, keeping business logic pure.

### 🛠️ Implementation in This Project

This project is structured into four main layers:

1️⃣ **Presentation Layer** → Handles HTTP requests and responses (Controllers, Middleware).  
2️⃣ **Application Layer** → Contains use cases, DTOs, and business logic.  
3️⃣ **Domain Layer** → Defines core business entities and interfaces.  
4️⃣ **Infrastructure Layer** → Handles data persistence, repositories, and database interactions.  

Each layer communicates **only through well-defined interfaces**, ensuring **loose coupling** and **high cohesion**.

By following Clean Architecture, this project is structured to **support future enhancements, improve testability, and remain adaptable to technological changes**.

---

## 🚀 How to Implement

### 🐳 Setting up PostgreSQL with Docker
To set up your PostgreSQL database using Docker, run the following command:

```bash
docker run --name task-management-db -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=Task-Management -p 5432:5432 -d postgres
```

### 🛠️ Configure Your Database Connection
After running the Docker command, update your `appsettings.json` file with the following connection string:

```json
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres"
}
```

✅ Now, you are **good to go!** Your database is running, and your application is ready to connect.

---

## **🚀 Features Implemented**
✅ **Clean Architecture** (Separation of Concerns)  
✅ **CQRS (Command-Query Responsibility Segregation)**  
✅ **PostgreSQL Database with EF Core**  
✅ **Dockerized Backend & Database**  
✅ **Logging with Serilog**  
✅ **Exception Handling Middleware**  
✅ **Task Management API Endpoints**  
✅ **Guid-based IDs for Tasks**  
✅ **GitHub Actions for CI/CD**  
✅ **Angular Frontend**  

---

## **📌 API Endpoints**
### **Task Endpoints**
| Method | Endpoint              | Description            |
|--------|-----------------------|------------------------|
| GET    | `/api/tasks`          | Get all tasks         |
| GET    | `/api/tasks/{id}`     | Get task by ID        |
| POST   | `/api/tasks`          | Create a new task     |
| PUT    | `/api/tasks/{id}`     | Update a task         |
| DELETE | `/api/tasks/{id}`     | Delete a task         |

---

## **🚀 Features NOT Implemented (Yet)**
🚧 **JWT Authentication (To Be Added)**  
🚧 **Unit Tests (To Be Added)**  
🚧 **Cloud Deployment (To Be Added)**  
🚧 **Task Assignments & Priorities (To Be Added)**  

---

## 🤝 Contributions

This project was built as a **self-learning project** to explore **Clean Architecture, C#, and ASP.NET Core development**.  
There is **no need to contribute** unless you find an issue, bug, or something that needs fixing.  
This is purely a **hobby project for educational purposes**

---

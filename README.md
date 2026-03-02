# 📚 Library Management System

A full-stack **Library Management System** built with **.NET 8 Web API** (backend) and **React + TypeScript** (frontend).

This application allows users to perform full **CRUD operations** on books with a clean, modern, and responsive UI.

---

## ✨ Features

* ✅ Create new books (Title, Author, Description)
* ✅ View all books with search & filter
* ✅ Update book details (inline editing)
* ✅ Delete books with confirmation modal
* ✅ Form validation with error messages
* ✅ Toast notifications for success & error feedback
* ✅ Responsive UI using Tailwind CSS
* ✅ Book count & search result counter

---

## 🛠️ Tech Stack

### 🔹 Backend

* C# – .NET 8 Web API
* Entity Framework Core
* SQLite Database
* Swagger / OpenAPI

### 🔹 Frontend

* React 18
* TypeScript
* Tailwind CSS
* Axios
* Lucide React Icons

---

## 🚀 Getting Started

### 📌 Prerequisites

Make sure you have installed:

* [.NET 8 SDK](https://dotnet.microsoft.com/download)
* [Node.js (v18 or higher)](https://nodejs.org/)
* [Git](https://git-scm.com/)

---

## 📥 Installation Guide

### 1️⃣ Clone the Repository

```bash
git clone https://github.com/sasiiii02/LIBRARY-SYSTEM.git
cd LIBRARY-SYSTEM
```

---

## ⚙️ Backend Setup

```bash
# Navigate to backend project
cd LibrarySystem/LibrarySystem

# Restore dependencies
dotnet restore

# Run the backend
dotnet run
```

🔹 Backend will run at:

```
https://localhost:7226
```

🔹 Swagger documentation:

```
https://localhost:7226/swagger
```

📌 Note:
The SQLite database (`library.db`) will be automatically created on first run.

---

## 🎨 Frontend Setup (Open New Terminal)

```bash
# Navigate to frontend folder
cd library-frontend

# Install dependencies
npm install

# Start development server
npm run dev
```

🔹 Frontend will run at:

```
http://localhost:5173
```

---

## 🌐 Access the Application

1. Make sure backend is running at:
   `https://localhost:7226`

2. Make sure frontend is running at:
   `http://localhost:5173`

3. Open your browser and visit:

```
http://localhost:5173
```

⚠️ Both backend and frontend must run simultaneously.

---

## 📁 Project Structure

```
LIBRARY-SYSTEM/
│
├── LibrarySystem/                  # Backend (.NET)
│   ├── LibrarySystem/
│   │   ├── Controllers/
│   │   │   └── BooksController.cs
│   │   ├── Data/
│   │   │   └── AppDbContext.cs
│   │   ├── Models/
│   │   │   └── Book.cs
│   │   ├── DTOs/
│   │   │   ├── CreateBookDto.cs
│   │   │   └── BookResponseDto.cs
│   │   └── Program.cs
│   └── LibrarySystem.sln
│
└── library-frontend/               # Frontend (React + TS)
    ├── src/
    │   ├── api/
    │   │   └── booksApi.ts
    │   ├── components/
    │   │   ├── BookForm.tsx
    │   │   ├── BookList.tsx
    │   │   └── ConfirmModal.tsx
    │   ├── context/
    │   │   └── ToastContext.tsx
    │   ├── types/
    │   │   └── Book.ts
    │   └── App.tsx
    ├── package.json
    └── tailwind.config.js
```

---

## 🔌 API Endpoints

| Method | Endpoint          | Description     |
| ------ | ----------------- | --------------- |
| GET    | `/api/books`      | Get all books   |
| GET    | `/api/books/{id}` | Get book by ID  |
| POST   | `/api/books`      | Create new book |
| PUT    | `/api/books/{id}` | Update book     |
| DELETE | `/api/books/{id}` | Delete book     |

---

## 🎯 Sample Book Data

Use this JSON to test the API:

```json
{
  "title": "To Kill a Mockingbird",
  "author": "Harper Lee",
  "description": "The unforgettable novel of a childhood in a sleepy Southern town."
}
```

```json
{
  "title": "Dune",
  "author": "Frank Herbert",
  "description": "Set on the desert planet Arrakis, Dune is the story of Paul Atreides."
}
```

```json
{
  "title": "The Midnight Library",
  "author": "Matt Haig",
  "description": "Between life and death there is a library."
}
```

---

## 🐛 Common Issues & Solutions

### ❌ CORS Error

**Problem:** Frontend cannot access backend
**Solution:** Ensure backend is running at `https://localhost:7226`

---

### ❌ Database Not Found

**Solution:**
The database is automatically created on first run.
Check inside:

```
LibrarySystem/LibrarySystem/library.db
```

---

### ❌ Frontend Cannot Connect to Backend

Check the API URL inside:

```
library-frontend/src/api/booksApi.ts
```

It should match:

```ts
const API_URL = "https://localhost:7226/api/books";
```

---

## 📊 Database Information

* Database: SQLite
* File Location: `LibrarySystem/LibrarySystem/library.db`
* Created Automatically: Yes
* Seed Data:

  * The Hobbit
  * 1984

---

## 👨‍💻 Author

**Sasanka Karunarathna**
GitHub: [https://github.com/sasiiii02](https://github.com/sasiiii02)

---

## 📄 License

This project was developed as part of a **Software Engineering Internship Assignment** for Expernetic.

---

## 🙏 Acknowledgments

* Expernetic for the internship opportunity
* Lucide React for icons
* Tailwind CSS for styling

---



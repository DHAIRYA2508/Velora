# Velora — Fine Dining Restaurant System

A luxury restaurant ordering platform with JWT authentication, role-based access, and a full admin dashboard.

---

## Brand

**Velora** — Fine Dining · India · Est. 2018

---

## Prerequisites

- [Node.js 18+](https://nodejs.org)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Angular CLI: `npm install -g @angular/cli@17`

---

## Getting Started

### 1. Backend

```cmd
cd backend
dotnet restore
dotnet run
```

- API runs at: http://localhost:5000
- Swagger docs: http://localhost:5000/swagger
- Database (`velora.db`) is auto-created on first run
- Default admin is auto-seeded

### 2. Frontend

```cmd
cd frontend
npm install
npm start
```

- App runs at: http://localhost:4200

---

## Demo Credentials

| Role     | Email              | Password   |
|----------|--------------------|------------|
| Admin    | admin@velora.com   | Admin@123  |
| Customer | Register a new account at /register |

---

## Access Control

| Route    | Public | Customer | Admin |
|----------|--------|----------|-------|
| /        | ✓      | ✓        | ✓     |
| /menu    | ✓      | ✓        | ✓     |
| /cart    | —      | ✓        | —     |
| /admin   | —      | —        | ✓     |

## API Endpoints

| Method | Route                    | Access         |
|--------|--------------------------|----------------|
| POST   | /api/auth/register       | Public         |
| POST   | /api/auth/login          | Public         |
| GET    | /api/menu                | Public         |
| POST   | /api/menu                | Admin only     |
| PUT    | /api/menu/:id            | Admin only     |
| DELETE | /api/menu/:id            | Admin only     |
| GET    | /api/order               | Admin only     |
| POST   | /api/order               | Authenticated  |
| PUT    | /api/order/:id/status    | Admin only     |

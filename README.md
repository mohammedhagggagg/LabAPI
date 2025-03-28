
# Day01 API

A RESTful API built with ASP.NET Core that manages Departments and Students, including authentication, role management, and exception handling.

## Features
- **User Authentication** (Login, Register, JWT Token)
- **Role Management** (Student, Admin)
- **CRUD Operations** for Departments and Students
- **Exception Handling Middleware**
- **Custom Validation**
- **File Upload (Student Images)**
- **Token-based Authorization**

## Technologies Used
- **ASP.NET Core 9**
- **Entity Framework Core** (Code First Migrations)
- **SQL Server**
- **JWT Authentication**
- **Identity Framework**

---
## Installation & Setup

### Prerequisites
- .NET 9 SDK
- SQL Server
- Visual Studio / VS Code

### Steps
1. **Clone the Repository**
   ```sh
   git clone https://github.com/your-repo-url.git
   cd Day01
   ```
2. **Update Connection String**
   Edit `appsettings.json` to configure the database connection.

3. **Run Migrations**
   ```sh
   dotnet ef database update
   ```

4. **Run the Application**
   ```sh
   dotnet run
   ```

5. **Test with Postman** or Swagger (default at `/swagger`).

---
## API Endpoints

### Authentication
| Method | Endpoint | Description |
|--------|---------|-------------|
| POST   | `/api/Student/register` | Register a new student |
| POST   | `/api/Student/login`    | User login |
| POST   | `/api/Student/addAdminRole` | Assign admin role (Admin only) |

### Departments
| Method | Endpoint | Description |
|--------|---------|-------------|
| GET    | `/api/Department/GetAll` | Get all departments |
| GET    | `/api/Department/{id}` | Get department by ID |
| POST   | `/api/Department/Add` | Create a department |
| PUT    | `/api/Department/{id}` | Update department |
| DELETE | `/api/Department/{id}` | Delete department |

### Students
| Method | Endpoint | Description |
|--------|---------|-------------|
| GET    | `/api/Student` | Get all students (Requires Authentication) |
| GET    | `/api/Student/{id}` | Get student by ID |
| POST   | `/api/Student` | Add a student |
| PUT    | `/api/Student/{id}` | Update student details |
| DELETE | `/api/Student/{id}` | Delete student |

---
## Project Structure
```
Day01/
├── Controllers/
│   ├── DepartmentController.cs
│   ├── StudentController.cs
│
├── Data/
│   ├── AppDbContext.cs
│
├── DTO/
│   ├── AddAdminRole.cs
│   ├── LoginUserDto.cs
│   ├── StudentDTO.cs
│
├── Models/
│   ├── Department.cs
│   ├── Student.cs
│
├── Repository/
│   ├── DepartmentRepository.cs
│   ├── StudentRepository.cs
│
├── Service/
│   ├── AppNameHeaderFilter.cs
│   ├── ExceptionMiddleware.cs
│
├── Migrations/
│
├── appsettings.json
└── Program.cs
```

---
## Authentication & Authorization
- Users must **register & login** to get a JWT token.
- Token must be included in the **Authorization** header as `Bearer <token>`.
- Admin role is required for certain endpoints.

## Exception Handling
- Custom middleware handles global exceptions.
- Example: `/api/Student/HAndelExeption` demonstrates exception handling.

## Contact
For any issues, open a GitHub issue or contact the maintainer.

🚀 Happy Coding!


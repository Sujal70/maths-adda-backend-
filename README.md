# Maths Adda Backend API

A .NET 8 Web API with enterprise-level layered architecture.

## Project Structure

```
webApi/
├── src/
│   ├── MathsAdda.Api/           # API Layer (Controllers, Middleware)
│   ├── MathsAdda.Business/      # Business Layer (Services, Interfaces)
│   ├── MathsAdda.Data/         # Data Layer (Entities, DbContext, Repositories)
│   └── MathsAdda.Common/       # Common (DTOs, Models, Interfaces)
└── MathsAdda.sln               # Solution file
```

## Architecture Layers

### 1. API Layer (`MathsAdda.Api`)
- **Controllers**: Handle HTTP requests/responses
- **DTOs**: Data Transfer Objects for API
- **Middleware**: Custom middleware components

### 2. Business Layer (`MathsAdda.Business`)
- **Services**: Business logic implementation
- **Interfaces**: Service contracts

### 3. Data Layer (`MathsAdda.Data`)
- **Entities**: Database models
- **Context**: Entity Framework DbContext
- **Repositories**: Data access patterns

### 4. Common Layer (`MathsAdda.Common`)
- **DTOs**: Shared data transfer objects
- **Models**: Common models
- **Interfaces**: Shared interfaces

## Setup Instructions

### 1. Restore Packages
```bash
cd src/MathsAdda.Api
dotnet restore
```

### 2. Update Database Connection
Edit `src/MathsAdda.Api/appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=MathsAdda;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True"
}
```

### 3. Create Database
```bash
dotnet ef database update
```

### 4. Run the API
```bash
dotnet run
```

### 5. Access Swagger
Open: `http://localhost:5000/swagger`

## API Endpoints

### Authentication
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/auth/login` | User login |
| POST | `/api/auth/register` | User registration |
| POST | `/api/auth/refresh-token` | Refresh JWT token |
| POST | `/api/auth/revoke-token` | Revoke refresh token |

### Courses
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/courses` | Get all courses |
| GET | `/api/courses/class/{level}` | Get courses by class |
| GET | `/api/courses/{id}` | Get course by ID |
| POST | `/api/courses` | Create course (Admin/Teacher) |
| PUT | `/api/courses/{id}` | Update course (Admin/Teacher) |
| DELETE | `/api/courses/{id}` | Delete course (Admin) |

### Students
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/students/profile` | Get student profile |
| POST | `/api/students/enroll/{courseId}` | Enroll in course |
| PUT | `/api/students/profile` | Update profile |

## Roles
- **Admin**: Full system access
- **Teacher**: Manage courses
- **Student**: Access courses, take tests
- **Parent**: Monitor child progress

## Technology Stack
- .NET 8
- Entity Framework Core 8
- JWT Authentication
- SQL Server
- Swagger/OpenAPI
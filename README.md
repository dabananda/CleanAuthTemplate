# Clean Architecture Auth Template

A production-ready ASP.NET Core Web API template built with **Clean Architecture** principles. This starter kit comes pre-configured with **Identity Authentication**, **JWT Security**, **Role Management**, **Email Verification**, **Serilog Logging**, and **Global Exception Handling**.

It is designed to be a robust starting point for enterprise-level applications, ensuring scalability, maintainability, and standard coding practices.

## 🚀 Features

* **Clean Architecture**: Strict separation of concerns (Domain, Application, Infrastructure, API).
* **Authentication & Authorization**:
    * ASP.NET Core Identity.
    * JWT Token Authentication (Access Tokens).
    * Role-Based Authorization (Admin, User, Manager).
* **Security**:
    * Standardized API Responses (`Succeeded`, `Data`, `Message`).
    * Global Exception Handling Middleware (hides stack traces in production).
    * CORS Policy Configuration.
* **Logging**:
    * **Serilog** integrated for structured logging.
    * Logs to **Console** (colored) and **File** (rolling daily intervals).
    * Request Logging (logs HTTP method, path, and duration).
* **Email System**:
    * MailKit integration for sending emails.
    * HTML Email Template support.
    * Email Verification flow.
* **Database**:
    * Entity Framework Core.
    * Automated Database Seeding (Admin User & Default Roles).

## 🛠 Tech Stack

* **Framework**: .NET 10 (Preview/Latest)
* **Language**: C#
* **Database**: Microsoft SQL Server
* **ORM**: Entity Framework Core
* **Auth**: ASP.NET Core Identity & JWT Bearer
* **Logging**: Serilog
* **Email**: MailKit
* **Documentation**: OpenAPI / Swagger (Built-in)

## 📂 Project Structure

The solution follows the Clean Architecture dependency rule (dependencies flow inward):

* **`CleanAuthTemplate.Domain`**:
    * Enterprise logic and entities (e.g., `ApplicationUser`).
    * No dependencies on other projects.
* **`CleanAuthTemplate.Application`**:
    * Business logic, Interfaces (`IEmailService`, `IJwtTokenGenerator`), and DTOs.
    * Depends on *Domain*.
* **`CleanAuthTemplate.Infrastructure`**:
    * External concerns (DbContex, Email Implementation, Identity, JWT generation).
    * Depends on *Application*.
* **`CleanAuthTemplate.API`**:
    * Presentation layer (Controllers, Middleware, Program.cs).
    * Depends on *Application* and *Infrastructure*.

## ⚡ Getting Started

### 1. Prerequisites
* [.NET SDK](https://dotnet.microsoft.com/download)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB or Docker)

### 2. Clone the Repository
```bash
git clone https://github.com/dabananda/CleanAuthTemplate.git
cd CleanAuthTemplate
```

### 3. Configuration

Update `CleanAuthTemplate.API/appsettings.json` with your settings:

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CleanAuthTemplateDb;Trusted_Connection=True;TrustServerCertificate=Yes;MultipleActiveResultSets=true"
  },
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "Username": "your-email@gmail.com",
    "Password": "your-app-password",
    "From": "your-email@gmail.com"
  },
  "JwtSettings": {
    "Secret": "your-very-secure-secret-key-minimum-32-chars",
    "Issuer": "CleanAuthTemplateAPI",
    "Audience": "CleanAuthTemplateClient",
    "DurationInMinutes": "60"
  },
  "AdminUser": {
    "Email": "admin@email.com",
    "Password": "Pass@123"
  },
  "DefaultUser": {
    "Email": "user@email.com",
    "Password": "Pass@123"
  },
  "Serilog": {
    "Using": [ "Serilog.Sinks.Console", "Serilog.Sinks.File" ],
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "theme": "Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme::Code, Serilog.Sinks.Console",
          "outputTemplate": "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      },
      {
        "Name": "File",
        "Args": {
          "path": "Logs/log-.txt",
          "rollingInterval": "Day",
          "retainedFileCountLimit": 7,
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      }
    ]
  },
  "AllowedHosts": "*"
}
```

### 4. Database Setup

Apply migrations and seed the database (creates default Roles and Admin user):

```
dotnet ef database update -s CleanAuthTemplate.API -p CleanAuthTemplate.Infrastructure
```

### 5. Run the Application

```
dotnet run --project CleanAuthTemplate.API
```

The API will start at `https://localhost:7078` (or similar).

## 📖 API Documentation

### Authentication

-   **POST** `/api/auth/register`: Register a new user (sends verification email).
    
-   **POST** `/api/auth/login`: Login to receive a JWT Token.
    
-   **GET** `/api/auth/verify-email`: Verify email using the token sent.
    

### Default Admin Credentials

-   **Email**: `admin@email.com`
    
-   **Password**: `Admin@123`
    

_(Note: These are created automatically by the `DbSeeder` on first run)._

## 🛡️ Response Format

All API responses follow this standard wrapper:

JSON

```
{
  "succeeded": true,
  "message": "Operation Successful",
  "errors": null,
  "data": { ... }
}
```

## 👤 Author

**Dabananda Mitra**

-   **Portfolio:** [dmitra.netlify.app](https://dmitra.netlify.app)
    
-   **LinkedIn:** [linkedin.com/in/dabananda](https://www.linkedin.com/in/dabananda/)
    
-   **Email:** dabananda.dev@gmail.com

-   **Facebook:** [fb.com/imdmitra](https://facebook.com/imdmitra)

-   **WhatsApp:** [+8801304080014](https://wa.me/8801304080014)

## 📄 License

This project is licensed under the MIT License - see the [LICENSE.txt](https://github.com/dabananda/CleanAuthTemplate/blob/master/LICENSE.txt) file for details.

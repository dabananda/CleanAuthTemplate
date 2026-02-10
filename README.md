# Clean Architecture Starter API Template

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

* **Framework**: .NET 10
* **Language**: C#
* **Database**: Microsoft SQL Server
* **ORM**: Entity Framework Core
* **Auth**: ASP.NET Core Identity & JWT Bearer
* **Logging**: Serilog
* **Email**: MailKit
* **Documentation**: OpenAPI (Built-in)

## 📂 Project Structure

The solution follows the Clean Architecture dependency rule (dependencies flow inward):

* **`CleanAuthTemplate.Domain`**:
    * Enterprise logic and entities (e.g., `ApplicationUser`).
    * No dependencies on other projects.
* **`CleanAuthTemplate.Application`**:
    * Business logic, Interfaces (`IEmailService`, `IJwtTokenGenerator`), and DTOs.
    * Depends on *Domain*.
* **`CleanAuthTemplate.Infrastructure`**:
    * External concerns (DbContext, Email Implementation, Identity, JWT generation).
    * Depends on *Application*.
* **`CleanAuthTemplate.API`**:
    * Presentation layer (Controllers, Middleware, Program.cs).
    * Depends on *Application* and *Infrastructure*.

---

## 📦 How to Use as a Template

You can install this project as a local .NET template to generate new solutions with a single command.

### 1. Download & Install the Template

Clone the repository and install it into your .NET CLI:
```bash
# Clone the repository
git clone https://github.com/dabananda/CleanAuthTemplate.git
cd CleanAuthTemplate

# Install the template locally
dotnet new install .
```

### 2. Create a New Project

Once installed, you can create a new solution anywhere on your machine. The template will automatically rename all files and namespaces (e.g., CleanAuthTemplate → MyNewApp).
```bash
# Create a directory for your new project
mkdir MyNewApp
cd MyNewApp

# Generate the solution (Replace 'MyNewApp' with your desired name)
dotnet new cleanauth -n MyNewApp
```

### 3. Setup Your New Project

After generating your project, follow these steps to get it running:

1. **Configure Settings**: Open `MyNewApp.API/appsettings.json` and update:
    * **ConnectionStrings**: Point to your local SQL Server.
    * **EmailSettings**: Add your SMTP credentials.
    * **JwtSettings**: Set a secure Secret key.

2. **Initialize Database**:
```bash
dotnet ef database update -s MyNewApp.API -p MyNewApp.Infrastructure
```

3. **Run the API**:
```bash
dotnet run --project MyNewApp.API
```

---

## ⚡ Running the Template Source (Development)

If you want to contribute to this template or run the source code directly without installing it:

### 1. Prerequisites

* .NET SDK
* SQL Server (LocalDB or Docker)

### 2. Clone the Repository
```bash
git clone https://github.com/dabananda/CleanAuthTemplate.git
cd CleanAuthTemplate
```

### 3. Configuration

Update `CleanAuthTemplate.API/appsettings.json` with your settings:
```json
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
```bash
dotnet ef database update -s CleanAuthTemplate.API -p CleanAuthTemplate.Infrastructure
```

### 5. Run the Application
```bash
dotnet run --project CleanAuthTemplate.API
```

The API will start at `https://localhost:7078` (or similar).

---

## 📖 API Documentation

### Authentication

* **POST** `/api/auth/register`: Register a new user (sends verification email).
* **POST** `/api/auth/login`: Login to receive a JWT Token.
* **GET** `/api/auth/verify-email`: Verify email using the token sent.

### Default Admin Credentials

* **Email**: admin@email.com
* **Password**: Admin@123

*(Note: These are created automatically by the DbSeeder on first run).*

---

## 🛡️ Response Format

All API responses follow this standard wrapper:
```json
{
  "succeeded": true,
  "message": "Operation Successful",
  "errors": null,
  "data": { ... }
}
```

---

## 👤 Author

**Dabananda Mitra**

* Portfolio: [dmitra.netlify.app](https://dmitra.netlify.app)
* LinkedIn: [linkedin.com/in/dabananda](https://linkedin.com/in/dabananda)
* Email: dabananda.dev@gmail.com
* WhatsApp: +8801304080014

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.
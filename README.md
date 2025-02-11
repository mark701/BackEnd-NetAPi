# Online Store Management System - ASP.NET Core API

## Overview
This is an ASP.NET Core API for managing an online store, including products, users, invoices, and invoice details. The API supports CRUD operations and is built using Entity Framework Core with database migrations.

## Features
- **User Management**: Register, authenticate, and manage users.
- **Product Management**: Add, update, delete, and retrieve products.
- **Invoice Management**: Create invoices with headers and detailed line items.
- **Database Migrations**: Uses Entity Framework Core migrations to manage database schema changes.
- Use Postman for Bearer Token authorization or other  similar tools after Creating a User
- If u don't have plz comment this part in each controller for ignore 401 Unauthorized : 
```sh
   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
```

## Technologies Used
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger (API Documentation)
- C#

## Installation & Setup
# 1. Clone the Repository
```sh
git clone https://github.com/your-repo/online-store-api.git
cd online-store-api
```

### 2. Configure Database
Update the `appsettings.json` file with your database connection string:
```json
"ConnectionStrings": {
   "DefaultConnection": "Server=localhost;Database=YourDataBase;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 3. Run Migrations
If using the command line:
```sh
dotnet ef database update
```
If using Microsoft Visual Studio:
```sh
Update-Database
```

### 4. Run the API
```sh
dotnet run
```

###

## API Endpoints
### User Management
| Method | Endpoint           | Description          |
|--------|------------------|----------------------|
| POST   | /api/User/UserRegister | Register a new user |
| POST   | /api/User/loginuser    | Authenticate user   |

### Product Management
| Method | Endpoint         | Description               |
|--------|----------------|-----------------------------|
| GET    | /api/Product/Get  | Get all products         |
| POST   | /api/Product/Save  | Create a new product    |
| PUT    | /api/Product/Update | Update a product       |
| DELETE | /api/Product/Delete/{id} | Delete a product  |

### Invoice Management
| Method | Endpoint               | Description              |
|--------|----------------------|----------------------------|
| POST   | /api/Invoice/Save       | Create a new invoice    |
| GET    | /api/Invoice/Get         | Get all invoices       |

## Running with Swagger
Once the API is running, access Swagger UI for testing at:
```
http://localhost:5000/swagger/index.html
```



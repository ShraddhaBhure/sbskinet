# E-Commerce Application - .NET FullStack Development Project
## Project Overview

This project is an E-Commerce application built using the .NET FullStack Development approach. It utilizes the repository pattern, with a generic repository pattern implemented using Entity Framework Core and a SQL Server database. The backend is powered by a C# Web API, providing a range of features including pagination, sorting, and filtering based on attributes like price, product type, and color. The frontend is developed using Angular CLI and styled using Bootstrap CSS and JavaScript.and .NET core Web API's are tested on Postman, and Swagger

![Project Image](https://github.com/ShraddhaBhure/sbskinet/blob/main/projects_images/skinetbasket.png)
![Project Image2](https://github.com/ShraddhaBhure/sbskinet/blob/main/projects_images/ecom-api's.PNG)

## Backend (C# Web API)

The backend of the application is built using C# and the .NET framework, providing RESTful APIs to communicate with the frontend and handle core business logic.

## Frontend (Angular CLI)

The frontend is developed using Angular CLI, a powerful command-line interface for Angular, delivering a responsive and interactive user interface.

## Authentication

- **JWT Token Authentication:**
  Utilizes JSON Web Tokens (JWT) for secure authentication, ensuring the safe transmission of information between parties.

## Containerization and Caching

- **Docker Containers:**
  Docker is employed for containerization, enabling consistent application deployment across different environments.

- **Redis-Server:**
  Redis is utilized as an in-memory data store and cache to enhance the overall performance of the application.

## User Management

- **.NET Core Identity:**
  Incorporates .NET Core Identity for managing user authentication, authorization, and related functionalities.

## E-Commerce Features

- **Payment Gateway:**
  Implements a secure payment gateway, allowing users to make online transactions for products or services.

- **Validations:**
  Includes validation mechanisms to ensure accurate and valid user input.

- **Filters:**
  Implements filters for sorting and organizing data, enhancing the user experience during product or content browsing.

## Testing and Documentation

- **Testing with Postman:**
  Utilizes Postman for comprehensive testing of API endpoints, covering various scenarios.

- **Documented using Swagger:**
  Swagger is employed to generate API documentation, providing a clear reference for developers to understand and interact with the API.


 ## Other Features

- **Repository Pattern**: The project architecture follows the repository pattern for effective data management.
- **Entity Framework Core**: The database interaction is implemented using Entity Framework Core, ensuring efficient data handling.
- **SQL Server Database**: The application uses a SQL Server database to store and manage E-Commerce data.
- **C# Web API**: The backend is developed using C# Web API, providing endpoints for various E-Commerce functionalities.
- **Angular CLI**: The frontend is built using Angular CLI, delivering a responsive and interactive user interface.
- **Bootstrap**: The application's styling is done using Bootstrap CSS and JavaScript for a modern and user-friendly design.
- **Postman**:Web API testing, GET, POST, PUT, DELETE, GET(by id).

## Prerequisites

Before you begin, ensure you have the following software and versions installed:
- Angular CLI: 16.1.4
- Node: 18.16.1
- Package Manager: npm 9.6.3
- Operating System: Windows (win32 x64)
  
# Development server
Run ng serve for a dev server. Navigate to http://localhost:4200/. The app will automatically reload if you change any of the source files.

# Code scaffolding
Run ng generate component component-name to generate a new component. You can also use ng generate directive|pipe|service|class|guard|interface|enum|module.

# Build
Run ng build to build the project. The build artifacts will be stored in the dist/ directory.

# Running unit tests
Run ng test to execute the unit tests via Karma.

# Running end-to-end tests
Run ng e2e to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.


# DealDesk

**DealDesk** is a full-stack application built to manage products, customers, and calculate dynamic discounted prices based on customized discount strategies agreed upon with customers. The backend is built with .NET and Entity Framework Core, using SQLite for data storage, while the frontend is developed using React with Material UI. This app demonstrates multi-level discount strategies, including volume and seasonal discounts, applied to product purchases.

## Table of Contents
1. [Features](#features)
2. [Technologies](#technologies)
3. [Application Architecture](#application-architecture)
4. [Getting Started](#getting-started)
5. [Running the Project](#running-the-project)
6. [API Documentation](#api-documentation)
7. [Frontend Usage](#frontend-usage)

## Features
- Manage products with attributes like name and standard price.
- Manage customers and their applicable discount strategies.
- Calculate discounts based on product quantity and customer-defined discount strategies.
- Multi-level discounting: supports volume and seasonal discounts.
- Interactive UI for managing products and customers, and calculating discounts.

## Technologies
### Backend
- **.NET 8**: The backend is built with .NET, using ASP.NET Core for the API.
- **Entity Framework Core**: For data access and ORM, utilizing SQLite for simplicity and ease of local setup.
- **SQLite**: Lightweight relational database for data persistence.
- **AutoMapper**: For mapping data transfer objects (DTOs) to entities.
- **MSTest & Moq**: Unit testing framework and mocking library for robust backend testing.

### Frontend
- **React (with Vite and TypeScript)**: A JavaScript library for building user interfaces, using Vite for fast builds and development, and TypeScript for type safety.
- **Material UI**: A popular React component library for building a responsive and modern interface with a consistent design language.
- **Axios**: For making HTTP requests to interact with the backend API.

## Application Architecture

The architecture of DealDesk follows a client-server model, where the frontend client communicates with the backend server through HTTP requests and responses in JSON format. The backend server is organized into three distinct layers: Presentation, Business Logic, and Data Access, ensuring a clear separation of concerns. The Presentation Layer handles incoming requests and prepares responses, the Business Logic Layer processes business rules and calculations (such as discount strategies), and the Data Access Layer manages interactions with the database. The application uses an SQLite database for data persistence, storing information about products, customers, and discount configurations.

![App_Architecture](https://github.com/user-attachments/assets/b3fa7499-f80e-4091-bf01-3aff14e452e1)

## Getting Started

### Prerequisites
1. **Node.js**: Required for running the frontend. [Download Node.js](https://nodejs.org/)
2. **.NET 8 SDK**: Required for running the backend. [Download .NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
3. **SQLite**: SQLite should be installed if you want to inspect the database directly. [Download SQLite](https://www.sqlite.org/download.html)

### Cloning the Repository
Clone the repository to your local machine:
```bash
git clone https://github.com/LukeX19/DealDesk
cd DealDesk
```

## Running the Project

### Backend (API)
1. Navigate to the API Project:
```bash
cd api/DealDesk/DealDesk.Api
```
2. Run the Backend:
- Start the API server:
```bash
dotnet run
```
- The API should be running at https://localhost:7268 (or another port if configured).
- If you encounter any issues related to dependencies, you can manually restore packages by running the following command before executing `dotnet run`:
```bash
dotnet restore
```

*Note: The application is configured to automatically apply migrations at startup, so no manual database setup is required.*

### Frontend (Client)
1. Navigate to the Frontend Project:
```bash
cd client/DealDesk
```
2. Install Dependencies:
```bash
npm install
```
3. Run the Frontend:
- Start the development server:
```bash
npm start
```
- The frontend should be running at http://localhost:5173 by default.

## API Documentation

The backend API provides endpoints for managing products, customers, and discounts. Once the backend is running, you can view and interact with the API documentation:
- Swagger UI: Visit https://localhost:7268/swagger to see the Swagger documentation for the API endpoints, including available requests, response formats, and error codes.

## Frontend Usage

The frontend provides an interface for interacting with the DealDesk application:

- Product Management: Add, edit, and delete products.
- Customer Management: Add, edit, and delete customers and assign discount strategies.
- Discount Calculation: Calculate the discounted price based on the selected product, customer, and quantity.

## Testing

### Running Backend Unit Tests

The backend contains unit tests for services and repositories, covering core business logic. To run these tests:
1. Navigate to the Test Project:
```bash
cd api/DealDesk/DealDesk.UnitTests
```
2. Run Tests:
```bash
dotnet test
```

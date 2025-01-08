# ECommercePlatform
An e-commerce platform built with ASP.NET Core and Entity Framework Core, featuring essential functionalities like product management, shopping cart, order placement, and user authentication. The application is fully deployed on Azure App Services with automated CI/CD pipelines using GitHub Actions.
Features
User Authentication: Secure login and registration system for customers.
Shopping Cart: Add, update, and remove items with quantity management.
Order Management: Place orders and view order history with detailed breakdowns.
Admin Functionality: Manage products, including adding, editing, and deleting items.
Responsive UI: Clean and user-friendly interface built using Razor Views.
Session Management: Session-based quantity management for an enhanced user experience.
Notifications: Interactive success and error notifications using TempData.
Technologies Used
Backend:
ASP.NET Core 9.0
Entity Framework Core
Frontend:
Razor Views
Bootstrap
Database:
Azure SQL Server
Deployment:
Azure App Services
CI/CD:
GitHub Actions
Testing:
xUnit for unit and integration tests
Getting Started
Prerequisites
.NET 9 SDK
SQL Server
Visual Studio 2022 or later
Setup Instructions
Clone the repository:

bash
Copy code
git clone https://github.com/yourusername/ecommerce-platform.git
cd ecommerce-platform
Update the database connection string in appsettings.json:

json
Copy code
"ConnectionStrings": {
    "DefaultConnection": "Your Azure SQL Server connection string here"
}
Apply database migrations:

bash
Copy code
dotnet ef database update
Run the application:

bash
Copy code
dotnet run
Navigate to https://localhost:5001 in your browser.
Testing
This project includes unit and integration tests to ensure reliability.

Run the tests:

bash
Copy code
dotnet test
CI/CD pipelines automatically execute the tests on every push or pull request.

Deployment
The application is deployed on Azure App Services. The CI/CD pipeline handles:

Building and testing the application.
Publishing the code to Azure App Services.
Applying database migrations automatically.
Screenshots
Product List Page

Cart Page

Future Enhancements
Add search and filtering functionality for products.
Implement role-based access for admin and users.
Add support for third-party payment gateways.
Contributing
Contributions are welcome! Feel free to fork the repository and submit a pull request.

License
This project is licensed under the MIT License.

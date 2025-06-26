🏥 Clinico – Medical Appointment Booking API
Clinico is the robust backend API powering the Clinico full-stack web application, designed for doctors and patients to manage appointments efficiently. Built with ASP.NET Core, the API supports secure authentication, role-based access, and seamless data integration with a PostgreSQL database. It follows RESTful principles and is containerized with Docker for ease of deployment and scaling.

Live Frontend: https://clinico-frontend.vercel.app/
Frontend Repo: Clinico Frontend on GitHub

🛠️ Technologies Used
ASP.NET Core 8

PostgreSQL with Entity Framework Core

Docker & Docker Compose

JWT Authentication

Swagger (API Documentation)

CORS Configuration

Role-based Authorization

Repository & Service Pattern

⚙️ Installation Instructions
Make sure you have .NET SDK, Docker, and PostgreSQL installed.

Clone the repository

bash
Copy
Edit
git clone https://github.com/Turkiano/clinico_backend.git
cd clinico_backend
Set up environment variables
Create a .env file or update appsettings.Development.json with your connection string and JWT secret.

Run the application

bash
Copy
Edit
dotnet run
Access API docs

bash
Copy
Edit
http://localhost:5000/swagger
Optional: Run with Docker

bash
Copy
Edit
docker-compose up --build

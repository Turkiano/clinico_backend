🏥 Clinico – Medical Appointment Booking API
-----------------------------------------------
Clinico is the robust backend API powering the Clinico full-stack web application, designed for doctors and patients to manage appointments efficiently. Built with ASP.NET Core, the API supports secure authentication, role-based access, and seamless data integration with a PostgreSQL database. It follows RESTful principles and is containerized with Docker for ease of deployment and scaling.

Live Frontend: https://clinico-frontend.vercel.app/
Frontend Repo: Clinico Frontend on GitHub



🛠️ Technologies Used
-----------------------------------
ASP.NET Core 8

PostgreSQL with Entity Framework Core

Docker & Docker Compose

JWT Authentication

Swagger (API Documentation)

CORS Configuration

Role-based Authorization

Repository & Service Pattern


⚙️ Installation Instructions
----------------------------------
Make sure you have .NET SDK, Docker, and PostgreSQL installed.

1) Clone the repository
git clone https://github.com/Turkiano/clinico_backend.git
cd clinico_backend


2) Set up environment variables
Create a .env file or update appsettings.Development.json with your connection string and JWT secret.


3) Run the application
dotnet run

4) Access API docs
http://localhost:5000/swagger

5) Optional: Run with Docker
docker-compose up --build


🧪 Usage Examples
---------------------------------------------------
Register a Patient
POST /api/auth/register
{
  "fullName": "Sarah Smith",
  "email": "sarah@example.com",
  "password": "Secure@123"
}


Book Appointment
POST /api/appointments
{
  "doctorId": 2,
  "appointmentDate": "2025-07-01T09:00:00"
}


Admin Login
POST /api/auth/login
{
  "email": "admin@clinico.com",
  "password": "Admin@123"
}



✨ Features and Functionality
------------------------------------------------
👨‍⚕️ Doctors: Add/edit availability, view schedules.

👩‍💼 Patients: Book, cancel, and track appointments.

🔐 Secure JWT Authentication with refresh token support.

🧩 Modular Architecture (Repository + Service pattern).

📄 Swagger Integration for API documentation.

🌍 CORS Support for frontend-backend integration.

🧪 Input Validation for all endpoints.



🤝 Contributing Guidelines
----------------------------------------------------
Fork the repository

Create your feature branch (git checkout -b feature/yourFeature)

Commit your changes (git commit -m 'Add your feature')

Push to the branch (git push origin feature/yourFeature)

Open a Pull Request



📄 License Information
This project is licensed under the MIT License.
See the LICENSE file for details.

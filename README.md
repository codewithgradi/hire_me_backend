# SmartCard: Digital Professional Identity & Recruitment Tool

SmartCard is a full-stack solution designed to modernize professional networking. It allows users to generate physical networking cards featuring dynamic QR codes. When scanned, these cards instantly bridge the gap between physical handouts and digital portfolios (LinkedIn, GitHub, or Personal Websites), empowering job seekers to share their credentials effortlessly.

---

## 🚀 The Mission
Traditional business cards are static and often end up in the trash. SmartCard transforms the physical card into a portal. By scanning a custom-generated QR code, recruiters get instant access to a candidate's live CV or portfolio, ensuring that your professional first impression is both high-tech and permanent.

## 🛠️ Tech Stack
- **Frontend:** Next.js (React), Tailwind CSS, Lucide Icons.
- **Backend:** .NET 9 Web API (Clean Architecture).
- **Database:** Entity Framework Core with SQL Server/PostgreSQL.
- **Authentication:** JWT (JSON Web Tokens) with Secure Password Hashing.
- **Tools:** Git, GitHub, Serilog, DotNetEnv.

---

## 🏗️ Architecture Overview
The backend is built following **Clean Architecture** principles to ensure high maintainability, testability, and scalability.

### 1. API (Presentation Layer)
- Handles HTTP requests and contains the Web API controllers.
- Responsible for managing the request-response lifecycle and global exception handling.

### 2. Application Layer
- Contains the business logic, service interfaces, and Use Cases.
- **DTOs & Mappers:** Handles data transformation between the Domain and API layers.
- **Interfaces:** Defines contracts for Infrastructure services.

### 3. Domain Layer
- The core of the system.
- Contains **Entities**, **Enums**, and domain-specific logic. 
- Independent of all other layers and external frameworks.

### 4. Infrastructure Layer
- **Persistence:** Implementation of Repositories and `AppDbContext`.
- **Identity:** Token Service for JWT generation and security.
- **Configuration:** Custom `Env` class for secure environment variable management.
- **Extensions:** Handles Dependency Injection registration.

---

## ✨ Features
- **Secure Auth:** Unified Login and Registration system.
- **Profile Management:** Detailed capture of academic and professional data.
- **Dynamic QR Generation:** Automatically generates a QR code linked to your preferred digital presence.
- **Print-Ready Cards:** Frontend layout optimized for physical card printing (Standard 3.5" x 2" ratio).

## 🔧 Getting Started
1. Clone the repository: `git clone https://github.com/yourusername/SmartCard.git`
2. Configure your `.env` file using `.env.example`.
3. Run the backend: `dotnet run --project API`
4. Run the frontend: `npm run dev`

# MedTime

A showcase healthcare appointment scheduling system demonstrating enterprise-grade .NET development practices.

[![.NET](https://github.com/peterm45/medtime/actions/workflows/dotnet.yml/badge.svg)](https://github.com/peterm45/medtime/actions/workflows/dotnet.yml)

## Features

- Patient management
- Doctor scheduling
- Appointment booking and management
- Rich domain model with business rules
- Clean Architecture implementation
- CQRS pattern with MediatR
- Enterprise-grade error handling
- Cross-platform compatibility with SQLite

## Technology Stack

- .NET 8
- Entity Framework Core
- SQLite Database
- MediatR for CQRS
- FluentValidation
- xUnit for testing
- Swagger for API documentation

## Architecture

This project follows Clean Architecture principles:

```
src/
├── MedTime.Domain/        # Enterprise business rules
│   ├── Entities/         # Domain entities
│   ├── ValueObjects/     # Value objects
│   └── Exceptions/       # Domain-specific exceptions
│
├── MedTime.Application/   # Application business rules
│   ├── Common/           # Shared interfaces
│   └── Features/         # CQRS commands and queries
│
├── MedTime.Infrastructure/# External concerns
│   ├── Persistence/      # Database implementation
│   └── Services/         # External services
│
└── MedTime.Api/          # Entry point and controllers
```

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- Any IDE that supports C# (VS Code, Visual Studio, or Rider)

### Installation

1. Clone the repository

```bash
git clone https://github.com/[your-username]/medtime.git
cd medtime
```

2. Restore dependencies

```bash
dotnet restore
```

3. Apply database migrations

```bash
cd src/MedTime.Api
dotnet ef database update
```

4. Run the application

```bash
dotnet run
```

The API will be available at `https://localhost:5001` with Swagger documentation at `/swagger`.

## API Endpoints

### Patients

- `POST /api/patients` - Create a new patient
- `GET /api/patients/{id}` - Get patient details

### Doctors

- `POST /api/doctors` - Create a new doctor
- `GET /api/doctors/{id}` - Get doctor details
- `GET /api/doctors/active` - List all active doctors

### Appointments

- `POST /api/appointments` - Schedule an appointment
- `GET /api/appointments/{id}` - Get appointment details

## Running Tests

```bash
dotnet test
```

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

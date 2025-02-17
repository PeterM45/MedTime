# MedTime

A showcase healthcare appointment scheduling system demonstrating enterprise-grade .NET development practices.

## 🏗️ Architecture

This project follows Clean Architecture principles to create a maintainable, testable, and scalable system.

```
├── src/
│   ├── MedTime.Domain/        # Enterprise/business rules
│   ├── MedTime.Application/   # Use cases/application logic
│   ├── MedTime.Infrastructure/# External concerns (DB, Email, etc.)
│   └── MedTime.Api/          # API endpoints and configuration
└── tests/
    ├── MedTime.UnitTests/
    └── MedTime.IntegrationTests/
```

### Domain Layer

- Rich domain model with enforced business rules
- Value objects for data integrity
- Domain events for flexibility

### Application Layer

- CQRS pattern for separation of concerns
- Fluent Validation for robust input validation
- Mediator pattern for loose coupling

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK
- SQL Server (or Docker)
- Your favorite IDE (Visual Studio 2022 or Rider recommended)

### Development Setup

1. Clone the repository

```bash
git clone https://github.com/yourusername/medtime.git
cd medtime
```

2. Run the database migrations

```bash
dotnet ef database update --project src/MedTime.Api
```

3. Start the API

```bash
dotnet run --project src/MedTime.Api
```

## 📝 Usage Example

```csharp
// Creating a new appointment
var patient = new Patient("John", "Doe",
    new DateOnly(1980, 1, 1),
    "john@example.com",
    "(555) 123-4567");

var doctor = new Doctor("Jane", "Smith",
    "Cardiology",
    "MD123456",
    "dr.jane@hospital.com",
    "(555) 987-6543");

var appointment = new Appointment(
    patient,
    doctor,
    DateTime.UtcNow.AddDays(1),
    DateTime.UtcNow.AddDays(1).AddHours(1)
);
```

## 🧪 Testing

```bash
dotnet test
```

## 🔒 Security Considerations

- Input validation at all layers
- Proper error handling
- Healthcare data privacy (HIPAA considerations)
- Audit logging for sensitive operations

## 📦 Deployment

This application can be deployed to:

- Azure App Service
- Docker containers
- Kubernetes clusters

Detailed deployment guides coming soon.

## 🤝 Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct and the process for submitting pull requests.

## 📜 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## ⭐ Next Steps

- [ ] Add appointment reminders
- [ ] Implement recurring appointments
- [ ] Add waitlist functionality
- [ ] Create admin dashboard
- [ ] Add reporting capabilities

# Apollo Aesthetics - Medical Clinic Management System

## 🏥 Overview

Apollo Aesthetics is a comprehensive medical clinic management system built with ASP.NET Core MVC using Clean Architecture principles. The system provides complete functionality for managing dental and aesthetic clinic operations including patient management, appointment scheduling, consultation requests, and administrative features.

## 🏗️ Architecture

This project follows **Clean Architecture** principles with clear separation of concerns:

- **Presentation Layer** (`ApolloAesthetics.Web`): MVC Controllers, Views, ViewModels
- **Application Layer** (`ApolloAesthetics.Application`): Business Logic, Services, DTOs
- **Domain Layer** (`ApolloAesthetics.Domain`): Entities, Interfaces, Business Rules
- **Infrastructure Layer** (`ApolloAesthetics.Infrastructure`): Data Access, External Services

## 🔧 Technologies Used

- **Framework**: ASP.NET Core 8.0 MVC
- **Database**: SQLite (for development) / SQL Server (for production)
- **ORM**: Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Frontend**: Bootstrap 5, jQuery, Font Awesome
- **Architecture**: Clean Architecture
- **Patterns**: Repository, Unit of Work, Dependency Injection

## 📊 Database Schema

The system includes the following main entities:

- **Users & Authentication**: ApplicationUser, ApplicationRole
- **Medical Staff**: Doctor, Patient
- **Services**: MedicalService, Appointment
- **Communication**: ConsultationRequest, Testimonial
- **Content**: GalleryImage, BeforeAfterImage, Content, SiteSetting

## 🚀 Features

### 👥 User Management
- Multi-role authentication (SuperAdmin, Admin, Doctor, Staff, Patient)
- User registration and profile management
- Role-based access control
- Password management

### 📅 Appointment Management
- Online appointment booking
- Doctor availability management
- Appointment status tracking
- Patient history

### 💬 Consultation System
- Online consultation requests
- Priority-based handling
- Response management
- Status tracking

### 📊 Dashboard & Analytics
- Administrative dashboard
- Statistics and reports
- Patient analytics
- Appointment metrics

### 🖼️ Content Management
- Gallery management
- Before/after images
- Testimonials
- Dynamic content

## 🛠️ Setup Instructions

### Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022 or VS Code
- SQL Server (optional, SQLite included for development)

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd ApolloAesthetics
   ```

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Update database connection string**
   - Open `src/ApolloAesthetics.Web/appsettings.json`
   - Update the connection string for your database

4. **Run database migrations**
   ```bash
   cd src/ApolloAesthetics.Web
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access the application**
   - Open browser and navigate to `https://localhost:5001`

## 👤 Default Users

The system seeds with default users:

- **SuperAdmin**: admin@apolloaesthetics.com / Admin123!
- **Doctor**: doctor@apolloaesthetics.com / Doctor123!
- **Staff**: staff@apolloaesthetics.com / Staff123!

## 📁 Project Structure

```
ApolloAesthetics/
├── src/
│   ├── ApolloAesthetics.Domain/          # Domain entities and interfaces
│   │   ├── Entities/                     # Domain entities
│   │   ├── Interfaces/                   # Repository interfaces
│   │   ├── Enums/                        # Enumerations
│   │   └── Constants/                    # Domain constants
│   │
│   ├── ApolloAesthetics.Application/     # Application services and DTOs
│   │   ├── Services/                     # Business logic services
│   │   ├── Interfaces/                   # Service interfaces
│   │   ├── DTOs/                         # Data transfer objects
│   │   ├── Mappings/                     # Object mappings
│   │   └── Common/                       # Common utilities
│   │
│   ├── ApolloAesthetics.Infrastructure/  # Data access and external services
│   │   ├── Data/                         # DbContext and configurations
│   │   ├── Repositories/                 # Repository implementations
│   │   └── Extensions/                   # Service registrations
│   │
│   └── ApolloAesthetics.Web/            # Presentation layer
│       ├── Controllers/                  # MVC Controllers
│       ├── Views/                        # Razor views
│       ├── Models/                       # View models
│       ├── Areas/                        # Admin area
│       └── wwwroot/                      # Static files
│
├── database_diagram.png                  # Database schema diagram
├── clean_architecture_diagram.png       # Architecture diagram
├── design_patterns_diagram.png          # Design patterns diagram
└── README.md                            # This file
```

## 🎨 Design Patterns Implemented

- **Repository Pattern**: Data access abstraction
- **Unit of Work Pattern**: Transaction management
- **Dependency Injection**: Service registration and resolution
- **MVC Pattern**: Separation of concerns in presentation
- **DTO Pattern**: Data transfer between layers
- **Result Pattern**: Standardized operation results
- **Factory Pattern**: Service creation
- **Strategy Pattern**: Authentication strategies

## 🔐 Security Features

- JWT-based authentication
- Role-based authorization
- Password hashing and validation
- CSRF protection
- Input validation and sanitization
- Secure cookie configuration

## 📱 Responsive Design

The application is fully responsive and works on:
- Desktop computers
- Tablets
- Mobile phones
- All modern browsers

## 🧪 Testing

To run tests:
```bash
dotnet test
```

## 📈 Performance Features

- Entity Framework query optimization
- Lazy loading where appropriate
- Caching strategies
- Pagination for large datasets
- Optimized database queries

## 🌐 Internationalization

The system supports:
- English (default)
- Arabic (partial support)
- RTL layout support

## 📝 API Documentation

API documentation is available at `/swagger` when running in development mode.

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 📞 Support

For support and questions:
- Email: support@apolloaesthetics.com
- Documentation: [Project Wiki]
- Issues: [GitHub Issues]

## 🔄 Version History

- **v1.0.0** - Initial release with core functionality
- **v1.1.0** - Added authentication and authorization
- **v1.2.0** - Enhanced UI/UX and responsive design

## 🎯 Future Enhancements

- [ ] Mobile application
- [ ] Payment integration
- [ ] SMS notifications
- [ ] Advanced reporting
- [ ] Multi-language support
- [ ] API versioning
- [ ] Real-time notifications

---

**Apollo Aesthetics** - Transforming smiles, enhancing lives. 🦷✨


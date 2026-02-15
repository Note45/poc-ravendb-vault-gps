# RavenDB Vault GRPS

This project is a **Proof of Concept (PoC)** built with **.NET**, aiming to demonstrate the application of the **Event Sourcing** pattern using **RavenDB** as the database.  

The PoC explores concepts such as event persistence, state rehydration, and query projections (read models), showing how RavenDB can be leveraged as an **Event Store** in distributed applications.

---

## 🚀 Goals

- Validate the use of **RavenDB** as an event store.  
- Demonstrate how to implement **Event Sourcing** in .NET.  
- Simulate write and read operations based on events.  
- Provide a starting point for applications adopting **event-driven architectures**.  

---

## 🏗️ Architecture

The PoC follows the principles of **CQRS + Event Sourcing**:

- **Command Side (Write Model)**: handles commands and generates events that describe state changes.  
- **Event Store (RavenDB)**: stores all events immutably.  
- **Projections (Read Model)**: built by applying events to provide optimized queries.  

---

## 🔧 Tech Stack

- [.NET](https://dotnet.microsoft.com/)  
- [RavenDB](https://ravendb.net/) (Event Store and Projections)  
- **gRPC** (service communication)  
- **CQRS(MediatR) + Event Sourcing**  
- **Swagger** (API documentation)
- **Validation** (FluentValidation)
- **Error Handling**: Pattern Result<T>
- **Automatic Validation**: ValidationBehavior 
- **DTOs**: Data Transfer Objects 
- **Repository Pattern**: Data access abstraction (RavenDB)

## 📁 Project Structure
```
Vault.Gps/
├── Application/                 # Application layer (CQRS)
│   ├── Commands/                # Command definitions
│   ├── CommandHandlers/         # Handlers for commands
│   ├── Queries/                 # Query definitions
│   ├── QueryHandlers/           # Handlers for queries
│   ├── DTOs/                    # Data Transfer Objects
│   ├── Behaviors/               # MediatR behaviors (validation)
│   └── Common/                  # Utilities (Result<T>, etc)
│
├── Controllers/                 # API Endpoints
│   └── GpsPositionController.cs
│
├── Domain/                      # Business rules
│   ├── Validators/              # FluentValidation validators
│   └── Enums/
│
├── Infra/                       # Infrastructure layer
│   └── Database/
│       ├── Repositories/        # Data access implementations
│       └── DocumentStoreHolder.cs # RavenDB session management
│
├── Contracts/                   # Shared interfaces and models
│   ├── Models/
│   ├── Services/
│   └── Enums/
│
├── Extensions/                  # Extension methods for DI
│   ├── ApplicatonService/
│   ├── Database/
│   └── Validations/
│
├── Program.cs                   # DI and middleware configuration
└──appsettings.json             # Configuration
```

## ⚙️ How to Run

### Prerequisites
- [.NET 9+ SDK](https://dotnet.microsoft.com/download)  
- [RavenDB](https://ravendb.net/downloads) running locally (can be via Docker).  

### Steps
1. Clone the repository:  
```bash
   git clone https://github.com/Note45/poc-ravendb-vault-gps.git
   cd ravendb-vault-grps
```

2. Create base vault-gps in RavenDB

3. Configure the RavenDB connection string in appsettings.json.

4. Run the project:

```bash 
  dotnet run --project Vault.Gps/Vault.Gps.csproj 
```

5. The RavenDB Vault GRPS API will be available at:

http://localhost:5247/swagger  


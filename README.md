# SafeShift

SafeShift is a Workplace Safety and Incident Management Web API built with ASP.NET Core and Entity Framework Core.

The project is designed to help organizations centralize:

- user registration and login
- incident reporting
- safety inspections
- shift tracking

Its goal is to improve safety, organization, and response time through a clean API-based backend.

## Project Scope

Main modules:

- Authentication
- Users
- Incidents
- Inspections
- Shifts

## Tech Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- AutoMapper
- Swagger / OpenAPI

## Solution Structure

- `SafeShift`
  The API project with controllers, startup configuration, Swagger, and app settings.

- `SafeShift.BLL`
  The business logic layer with services and mapping profile.

- `SafeShift.DAL`
  The data access layer with `SafeShiftDbContext`, repositories, and migrations.

- `SafeShift.Models`
  Entity models, DTOs, validation attributes, and shared response models.

## Architecture

The project follows a layered structure:

1. Controllers in `SafeShift` handle HTTP requests and responses.
2. Services in `SafeShift.BLL` contain business logic.
3. Repositories in `SafeShift.DAL` handle database access.
4. DTOs in `SafeShift.Models` separate API contracts from entity classes.

This keeps controllers thin and separates responsibilities clearly.

## Core Features

- basic user registration and login
- hashed password storage
- CRUD operations for incidents
- CRUD operations for inspections
- CRUD operations for shifts
- user lookup endpoints
- filtering support for:
  - incidents by severity, user, and date
  - inspections by date
  - shifts by user and date
- Swagger UI for endpoint exploration and testing

## Data Model

### User

- `UserId`
- `Name`
- `Email`
- `PasswordHash`
- `Role`

### Incident

- `IncidentId`
- `Description`
- `Date`
- `Severity`
- `UserId`

### Inspection

- `InspectionId`
- `Date`
- `Notes`
- `UserId`

### Shift

- `ShiftId`
- `StartTime`
- `EndTime`
- `UserId`

Relationships:

- one `User` has many `Incidents`
- one `User` has many `Inspections`
- one `User` has many `Shifts`

## Validation Highlights

The API includes request validation for common bad input cases, including:

- required registration fields
- valid email format
- minimum password length
- required incident description and severity
- allowed incident severity values
- required inspection notes
- required shift times
- `EndTime` later than `StartTime`

## API Endpoints

### Auth

- `POST /api/auth/register`
- `POST /api/auth/login`

### Users

- `GET /api/users`
- `GET /api/users/{userId}`
- `DELETE /api/users/{userId}`

### Incidents

- `GET /api/incidents`
- `GET /api/incidents/{incidentId}`
- `POST /api/incidents`
- `PUT /api/incidents/{incidentId}`
- `DELETE /api/incidents/{incidentId}`

Query parameters:

- `severity`
- `userId`
- `date`

### Inspections

- `GET /api/inspections`
- `GET /api/inspections/{inspectionId}`
- `POST /api/inspections`
- `PUT /api/inspections/{inspectionId}`
- `DELETE /api/inspections/{inspectionId}`

Query parameters:

- `date`

### Shifts

- `GET /api/shifts`
- `GET /api/shifts/{shiftId}`
- `POST /api/shifts`
- `PUT /api/shifts/{shiftId}`
- `DELETE /api/shifts/{shiftId}`

Query parameters:

- `userId`
- `date`

## Database

Entity Framework Core is configured with SQL Server through:

- [Program.cs](./SafeShift/Program.cs)
- [appsettings.json](./SafeShift/appsettings.json)
- [SafeShiftDbContext.cs](./SafeShift.DAL/Data/SafeShiftDbContext.cs)

Initial migration is already included in:

- [Migrations](./SafeShift.DAL/Migrations)

## How To Run

From the solution root:

```bash
dotnet run --project SafeShift/SafeShift.csproj
```

Swagger should be available at:

- `http://localhost:5196/swagger`
- or `https://localhost:7208/swagger`

## How To Build

```bash
dotnet build SafeShift.sln
```

## Testing the API

A simple order for manual testing is:

1. Register a user with `POST /api/auth/register`
2. Log in with `POST /api/auth/login`
3. Use the returned `userId` to:
   - create incidents
   - create inspections
   - create shifts
4. Test filtering with query parameters from Swagger

## Swagger

Swagger / OpenAPI is enabled in the API project and includes:

- endpoint summaries
- request and response models
- register example request body
- login example request body

## Notes

- Authentication is basic email/password validation only
- JWT is not used
- Passwords are hashed before storage
- Swagger is the easiest way to test endpoints during development

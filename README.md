# Theo's Food Trailer Crew Management App

A C# and .NET console application for managing food trailer crew members.

The application demonstrates object-oriented programming, JSON data persistence, CRUD operations, service-based architecture, and automated testing with xUnit.

## Features

- View all crew members
- Search for a crew member by name
- Add new crew members
- Update existing crew members
- Remove crew members
- Filter crew members by shift
- View certified crew members
- Store crew data in JSON
- Automatically assign IDs to new crew members
- Automated unit testing with xUnit

## CRUD Operations

| CRUD Operation | Application Feature |
|---|---|
| Create | Add crew member |
| Read | View all, search by name, filter by shift, view certified crew |
| Update | Update crew member |
| Delete | Remove crew member |

## Technologies Used

| Technology | Purpose |
|---|---|
| C# | Application programming language |
| .NET 10 | Application framework |
| System.Text.Json | JSON serialization and deserialization |
| LINQ | Searching and filtering crew data |
| xUnit | Automated testing |
| Git | Version control |
| GitHub | Source-code hosting |
| Visual Studio Code | Development environment |

## Project Structure

```text
FoodTrailerJsonApp/
│
├── Data/
│   └── crew.json
│
├── Models/
│   └── CrewMember.cs
│
├── Services/
│   └── CrewService.cs
│
├── FoodTrailerJsonApp.Tests/
│   ├── CrewServiceTest.cs
│   └── FoodTrailerJsonApp.Tests.csproj
│
├── Program.cs
├── FoodTrailerJsonApp.csproj
├── FoodTrailerJsonApp.sln
├── .gitignore
└── README.md
```

## Application Architecture

The application separates responsibilities into different parts of the project:

### Model

`CrewMember.cs` represents a crew member and contains properties such as:

- ID
- Name
- Role
- Shift
- Certification status

### Service

`CrewService.cs` contains the application's data-management logic.

The service handles operations such as:

- Loading crew members from JSON
- Saving crew members to JSON
- Finding crew members
- Adding crew members
- Updating crew members
- Removing crew members
- Filtering by shift
- Filtering certified crew members

### Data

`crew.json` acts as the application's persistent data store.

Instead of losing the crew information when the application closes, changes are serialized to JSON and saved to the file.

### Console Interface

`Program.cs` provides the user interface and communicates with `CrewService`.

The main menu allows the user to perform the application's CRUD operations.

## Main Menu

```text
================================
   THEO'S FOOD TRAILER CREW
================================

1. View all crew members
2. View crew member by name
3. Add crew member
4. Update crew member
5. Remove crew member
6. View crew by shift
7. View certified crew
8. Exit
```

## Automated Testing

The project includes an xUnit test project for testing the `CrewService`.

Current tests verify:

1. Missing JSON files return an empty crew list
2. Adding a crew member assigns an ID and saves the member
3. Crew members can be found by ID
4. Name searches are case-insensitive
5. Crew member information can be updated
6. Crew members can be removed
7. Crew members can be filtered by shift
8. Certified crew members can be retrieved

Current test result:

```text
Test summary: total: 8, failed: 0, succeeded: 8, skipped: 0
```

## Running the Application

### Prerequisites

Install the .NET SDK.

Verify your installation:

```bash
dotnet --version
```

### Clone the Repository

```bash
git clone https://github.com/tabner0320/FoodTrailerJsonApp.git
cd FoodTrailerJsonApp
```

### Run the Application

```bash
dotnet run
```

## Running the Tests

Run:

```bash
dotnet test FoodTrailerJsonApp.Tests/FoodTrailerJsonApp.Tests.csproj
```

A successful test run should report all tests passing.

## What I Learned

Building this project helped strengthen my understanding of:

- C# classes and objects
- Object-oriented programming
- Separating models and services
- CRUD operations
- Reading and writing JSON
- Serialization and deserialization
- LINQ queries
- File handling
- Automated testing with xUnit
- Test isolation using temporary files
- Git and GitHub workflows
- Resolving Git branch and rebase issues

## Portfolio Summary

This project demonstrates how I can take a simple console application and organize it into a more maintainable application with separate models, services, persistent JSON data, CRUD functionality, and automated testing.

It represents practical experience with C#, .NET, software architecture, data persistence, testing, and version control.
# Library Management System

A multi-layer **ASP.NET Core MVC** application for managing books, categories, library members, and borrowing/returning records. This project was built as a hands-on practice application for MVC, Entity Framework Core, Repository Pattern, Service Layer, Dependency Injection, validation, LINQ, and relational database concepts.

## Features

- Manage categories: add, edit, delete, and view
- Manage books: add, edit, delete, search, categorize, and track availability
- Manage members: add, edit, delete, and validate member details
- Borrow available books
- Prevent borrowing of unavailable books
- Return borrowed books
- Automatically update book availability
- View all borrowing history
- View active borrowings
- Store borrowing and return date/time
- Entity Framework Core with SQL Server
- Data Annotations validation
- Dependency Injection
- Repository Pattern
- Service Layer with business logic
- Multi-project architecture using Class Libraries/DLLs
- Razor Views and MVC Tag Helpers
- Conventional MVC routing
- Bootstrap-based UI

## Architecture

```text
                    LibraryManagement.Web
                           |
                           v
                LibraryManagement.Services
                           |
                           v
              LibraryManagement.DataAccess
                           |
                           v
                       SQL Server
```

### Presentation Layer

**LibraryManagement.Web**

Contains:
- Controllers
- Razor Views
- MVC model binding
- Data validation
- Navigation and UI
- Dependency Injection configuration

### Service Layer

**LibraryManagement.Services**

Contains:
- Service interfaces
- Service implementations
- Business logic

The borrowing functionality is handled here rather than directly inside the controller.

### Data Access Layer

**LibraryManagement.DataAccess**

Contains:
- Entity models
- DbContext
- Repository interfaces
- Repository implementations
- EF Core configuration
- Database migrations

The DataAccess and Services projects are Class Libraries and produce DLL assemblies.

## Database Design

The application uses four main tables.

### Categories

```text
CategoryId       Primary Key
CategoryName
```

### Books

```text
BookId           Primary Key
Title
Author
ISBN
CategoryId       Foreign Key
IsAvailable
```

### Members

```text
MemberId         Primary Key
MemberName
Email
Phone
```

### Borrowings

```text
BorrowingId      Primary Key
BookId           Foreign Key
MemberId         Foreign Key
BorrowDate
ReturnDate
```

### Relationships

```text
Category
    1
    |
    *
   Book
    |
    *
Borrowing
    *
    |
    1
  Member
```

A category can contain many books.

A book can have many borrowing records over its lifetime.

A member can have many borrowing records.

## Project Structure

```text
LibraryManagement
|
+-- LibraryManagement.Web
|   |
|   +-- Controllers
|   |   +-- HomeController.cs
|   |   +-- CategoryController.cs
|   |   +-- BookController.cs
|   |   +-- MemberController.cs
|   |   +-- BorrowingController.cs
|   |
|   +-- Views
|   |   +-- Home
|   |   +-- Category
|   |   +-- Book
|   |   +-- Member
|   |   +-- Borrowing
|   |   +-- Shared
|   |
|   +-- Program.cs
|
+-- LibraryManagement.Services
|   |
|   +-- Interfaces
|   |   +-- ICategoryService.cs
|   |   +-- IBookService.cs
|   |   +-- IMemberService.cs
|   |   +-- IBorrowingService.cs
|   |
|   +-- Services
|       +-- CategoryService.cs
|       +-- BookService.cs
|       +-- MemberService.cs
|       +-- BorrowingService.cs
|
+-- LibraryManagement.DataAccess
    |
    +-- Models
    |   +-- Category.cs
    |   +-- Book.cs
    |   +-- Member.cs
    |   +-- Borrowing.cs
    |
    +-- Interfaces
    |   +-- ICategoryRepository.cs
    |   +-- IBookRepository.cs
    |   +-- IMemberRepository.cs
    |   +-- IBorrowingRepository.cs
    |
    +-- Repositories
    |   +-- CategoryRepository.cs
    |   +-- BookRepository.cs
    |   +-- MemberRepository.cs
    |   +-- BorrowingRepository.cs
    |
    +-- Migrations
    +-- LibraryDbContext.cs
```

## Technologies Used

- C#
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Razor Views
- Bootstrap
- LINQ
- Data Annotations
- Dependency Injection
- Repository Pattern
- Service Layer
- Class Libraries / DLLs

## NuGet Packages

The Data Access project uses:

```text
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
```

## Entity Framework Core

The `LibraryDbContext` contains:

```csharp
public DbSet<Category> Categories { get; set; }
public DbSet<Book> Books { get; set; }
public DbSet<Member> Members { get; set; }
public DbSet<Borrowing> Borrowings { get; set; }
```

Relationships are configured using EF Core Fluent API.

Example:

```csharp
modelBuilder.Entity<Book>()
    .HasOne(b => b.Category)
    .WithMany(c => c.Books)
    .HasForeignKey(b => b.CategoryId)
    .OnDelete(DeleteBehavior.Restrict);
```

`DeleteBehavior.Restrict` is used to protect related data and preserve borrowing history.

## Repository Pattern

Each major entity has its own repository interface and implementation.

Example:

```text
IBookRepository
       |
       v
BookRepository
       |
       v
LibraryDbContext
       |
       v
SQL Server
```

Repositories handle database operations such as:

- Add
- Get
- Update
- Delete
- Search
- Filtering

## Service Layer

The Service Layer sits between controllers and repositories.

Example:

```text
BookController
      |
      v
IBookService
      |
      v
BookService
      |
      v
IBookRepository
      |
      v
BookRepository
```

The borrowing workflow demonstrates actual business logic in the Service Layer.

### Borrowing

```text
Book available?
      |
      v
Member exists?
      |
      v
Create Borrowing
      |
      v
Set Book.IsAvailable = false
```

### Returning

```text
Find Borrowing
      |
      v
Set ReturnDate
      |
      v
Find Book
      |
      v
Set Book.IsAvailable = true
```

## Validation

Data Annotations are used on the entity classes.

Example:

```csharp
[Required]
[StringLength(100)]
public string Title { get; set; }
```

Validation is handled through MVC's `ModelState`.

Razor views use validation Tag Helpers such as:

```cshtml
<span asp-validation-for="Title"
      class="text-danger">
</span>
```

## Routing

The application uses conventional ASP.NET Core MVC routing.

The default route is:

```text
{controller=Home}/{action=Index}/{id?}
```

Examples:

```text
/
/Book
/Category
/Member
/Borrowing
```

## Running the Project

### 1. Clone the repository

```bash
git clone <your-repository-url>
```

### 2. Open the solution

Open:

```text
LibraryManagement.sln
```

in Visual Studio.

### 3. Configure SQL Server

Update the SQL Server connection string in:

```text
LibraryManagement.DataAccess/LibraryDbContext.cs
```

Example:

```csharp
optionsBuilder.UseSqlServer(
    "Server=YOUR_SERVER;Database=LibraryManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
);
```

Do not commit credentials or sensitive connection strings to a public repository.

### 4. Apply migrations

Open the Package Manager Console and run:

```powershell
Update-Database
```

If required, specify the projects:

```powershell
Update-Database `
    -Project LibraryManagement.DataAccess `
    -StartupProject LibraryManagement.Web
```

### 5. Run

Set:

```text
LibraryManagement.Web
```

as the startup project and run the application.

## Main Routes

| Feature | Route |
|---|---|
| Home | `/` |
| Books | `/Book` |
| Add Book | `/Book/Add` |
| Categories | `/Category` |
| Add Category | `/Category/Add` |
| Members | `/Member` |
| Add Member | `/Member/Add` |
| Borrowings | `/Borrowing` |
| Borrow Book | `/Borrowing/Borrow` |
| Active Borrowings | `/Borrowing/Active` |

## Learning Objectives

This project was created to practice:

- ASP.NET Core MVC
- Controllers
- Dependency Injection
- Views
- Razor syntax
- MVC model binding
- Data Annotation validation
- Conventional routing
- Entity Framework Core
- SQL Server
- One-to-many relationships
- Foreign keys
- Navigation properties
- `Include()` / eager loading
- LINQ
- Repository Pattern
- Service Layer
- Business logic
- Class Libraries
- DLL-based multi-layer architecture
- CRUD operations
- Exception handling
- MVC data passing with ViewBag/ViewData
- Bootstrap and Razor Tag Helpers

## Future Improvements

Possible future improvements include:

- Authentication and authorization
- Role-based access control
- ASP.NET Core Identity
- Custom Tag Helpers
- More advanced state management
- Pagination
- Advanced search and filtering
- Dashboard statistics
- Fine calculation for overdue books
- Book reservation functionality
- Unit testing
- Integration testing
- OWASP-focused security improvements

## Project Status

**Completed**

The current version successfully demonstrates a multi-layer ASP.NET Core MVC Library Management System with:

- Multiple related database tables
- EF Core
- SQL Server
- Repository Pattern
- Service Layer
- Dependency Injection
- MVC Controllers and Views
- CRUD operations
- Validation
- Book search
- Borrowing and returning workflows
- Availability tracking
- Class Library/DLL architecture

## Author

**Mukul Deshwal**

Built as a hands-on ASP.NET Core MVC learning project.

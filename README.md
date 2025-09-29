# BookCrudApp

## Overview
A fullstack CRUD application for managing books built with React (frontend) and ASP.NET Core (backend). The application provides a complete solution for book management with persistent storage using PostgreSQL database.

## Features
- **Full CRUD Operations**: List, Add, Edit, and Delete books
- **Persistent Storage**: Using Entity Framework Core with PostgreSQL
- **API-First Architecture**: Fully separated frontend and backend with CORS enabled
- **Environment Configuration**: Secure handling of secrets and configurable URLs
- **TypeScript Support**: Full type safety in the frontend
- **Modern UI**: Clean, responsive interface built with React

## Tech Stack

### Frontend
- **React 19.1.1** - Modern UI library
- **TypeScript 4.9.5** - Type safety and better development experience
- **React Router DOM 7.9.1** - Client-side routing
- **CSS3** - Styling and responsive design

### Backend
- **ASP.NET Core 8** - Modern web framework
- **Entity Framework Core 8.0.0** - ORM for database operations
- **Npgsql 8.0.0** - PostgreSQL provider for EF Core
- **Swagger/OpenAPI** - API documentation

### Database
- **PostgreSQL** - Robust relational database

### Development Tools
- **Cursor** - AI-powered code editor
- **GitHub** - Version control and collaboration
- **pgAdmin** - PostgreSQL administration

## Project Structure

```
BookCrudApp/
├── frontend/                 # React TypeScript application
│   ├── src/
│   │   ├── api/             # API integration layer
│   │   │   └── books.ts     # Book API functions
│   │   ├── components/      # React components
│   │   │   ├── BookList.tsx # Book listing component
│   │   │   ├── AddBook.tsx  # Add book form
│   │   │   └── EditBook.tsx # Edit book form
│   │   ├── types/           # TypeScript type definitions
│   │   │   └── Book.ts      # Book interface
│   │   ├── App.tsx          # Main application component
│   │   └── index.tsx        # Application entry point
│   ├── public/              # Static assets
│   ├── package.json         # Frontend dependencies
│   └── .env                 # Environment variables
├── backend/                  # ASP.NET Core API
│   ├── Controllers/         # API controllers
│   │   └── BooksController.cs
│   ├── Data/               # Data access layer
│   │   └── AppDbContext.cs
│   ├── Models/             # Data models
│   │   └── Book.cs
│   ├── Migrations/         # EF Core migrations
│   ├── Properties/         # Configuration files
│   │   └── launchSettings.json
│   ├── Program.cs          # Application entry point
│   ├── appsettings.json    # Application configuration
│   └── backend.csproj      # Project dependencies
└── README.md               # This file
```

## Setup Instructions

### Prerequisites
- **Node.js** (v16 or higher)
- **.NET 8 SDK**
- **PostgreSQL** (v12 or higher)
- **Git**

### 1. Clone the Repository
```bash
git clone https://github.com/YOURUSERNAME/BookCrudApp.git
cd BookCrudApp
```

### 2. Database Setup
1. Install PostgreSQL and create a database:
   ```sql
   CREATE DATABASE BookCrudApp;
   ```

2. Update connection string in `backend/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=BookCrudApp;Username=your_username;Password=your_password"
     }
   }
   ```

### 3. Backend Setup
```bash
cd backend
dotnet restore
dotnet ef database update
dotnet run
```

The API will be available at `http://localhost:5239`
- Swagger UI: `http://localhost:5239/swagger`

### 4. Frontend Setup
```bash
cd frontend
npm install
npm start
```

The frontend will be available at `http://localhost:3000`

## Configuration

### Environment Variables

#### Frontend (.env)
```env
REACT_APP_API_URL=http://localhost:5239
```

#### Backend
The backend supports configuration through:
- **appsettings.json**: Default configuration
- **Environment Variables**: Override for different environments
  - `ASPNETCORE_URLS`: Override the server URL/port
  - `ASPNETCORE_ENVIRONMENT`: Set the environment (Development/Production)

### Backend Configuration
The backend uses Kestrel configuration in `appsettings.json`:
```json
{
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://localhost:5239"
      }
    }
  }
}
```

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/books` | Get all books |
| GET | `/books/{id}` | Get a specific book |
| POST | `/books` | Create a new book |
| PUT | `/books/{id}` | Update a book |
| DELETE | `/books/{id}` | Delete a book |

### Request/Response Examples

#### Create Book
```http
POST /books
Content-Type: application/json

{
  "title": "The Great Gatsby",
  "author": "F. Scott Fitzgerald",
  "pages": 180
}
```

#### Update Book
```http
PUT /books/1
Content-Type: application/json

{
  "title": "The Great Gatsby (Updated)",
  "author": "F. Scott Fitzgerald",
  "pages": 200
}
```

## Development

### Running in Development Mode
1. Start PostgreSQL service
2. Run backend: `cd backend && dotnet run`
3. Run frontend: `cd frontend && npm start`

### Building for Production
```bash
# Frontend
cd frontend
npm run build

# Backend
cd backend
dotnet publish -c Release
```

## Security Features
- **CORS Configuration**: Properly configured for frontend-backend communication
- **Environment Variables**: Sensitive data handled through environment variables
- **User Secrets**: Development secrets managed through .NET User Secrets
- **Configuration Management**: Secure configuration handling for different environments

## Contributing
1. Fork the repository
2. Create a feature branch: `git checkout -b feature/new-feature`
3. Commit changes: `git commit -m 'Add new feature'`
4. Push to branch: `git push origin feature/new-feature`
5. Submit a pull request

## License
This project is licensed under the MIT License - see the LICENSE file for details.

## Support
For support and questions, please open an issue in the GitHub repository.

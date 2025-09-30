# BookCrudApp

## Overview
A fullstack CRUD application for managing books built with React (frontend) and ASP.NET Core (backend). The application provides a complete solution for book management with persistent storage using PostgreSQL database.

## Features
- **Full CRUD Operations**: List, Add, Edit, and Delete books
- **AI-Powered Search**: Semantic search using free open-source embeddings
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
- **Hugging Face API** - Free AI embeddings for semantic search
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
│   │   │   ├── EditBook.tsx # Edit book form
│   │   │   └── SearchBooks.tsx # AI-powered search component
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
│   ├── Services/           # Business logic services
│   │   └── EmbeddingService.cs # AI embedding service
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

## AI-Powered Search Features

### How It Works
The application uses **Hugging Face's free API** with the `sentence-transformers/all-MiniLM-L6-v2` model to generate embeddings for semantic search. This enables users to search for books using natural language queries.

### Setup for AI Features

1. **Generate Embeddings**: 
   - Navigate to the "AI Search" page in the frontend
   - Click "Generate AI Embeddings" to create embeddings for all existing books
   - This process uses the free Hugging Face API (no API key required)

2. **Search Books**:
   - Use the search bar to find books by content, genre, or topic
   - The AI will return the most semantically similar books
   - Results are ranked by similarity score

### Technical Details
- **Embedding Model**: `sentence-transformers/all-MiniLM-L6-v2` (384 dimensions)
- **Similarity Algorithm**: Cosine similarity
- **Storage**: Embeddings stored as JSON in PostgreSQL
- **API**: Free Hugging Face Inference API (no authentication required)

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/books` | Get all books |
| GET | `/books/{id}` | Get a specific book |
| POST | `/books` | Create a new book |
| PUT | `/books/{id}` | Update a book |
| DELETE | `/books/{id}` | Delete a book |
| GET | `/books/search?q={query}` | AI-powered semantic search |
| POST | `/books/generate-embeddings` | Generate embeddings for all books |

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

## Deployment

### Backend Deployment (Render)

1. **Connect to Render**:
   - Sign up at [render.com](https://render.com)
   - Connect your GitHub repository
   - Select "New Web Service"

2. **Configure Backend Service**:
   - **Build Command**: `cd backend && dotnet publish -c Release -o /app/publish`
   - **Start Command**: `cd backend && dotnet backend.dll`
   - **Environment**: Docker
   - **Dockerfile Path**: `./backend/Dockerfile`

3. **Set Environment Variables**:
   ```
   ASPNETCORE_ENVIRONMENT=Production
   ASPNETCORE_URLS=http://0.0.0.0:8080
   DB_HOST=<your-postgres-host>
   DB_PORT=5432
   DB_NAME=<your-database-name>
   DB_USER=<your-database-user>
   DB_PASSWORD=<your-database-password>
   ```

4. **Database Setup**:
   - Create a PostgreSQL database on Render
   - Use the connection details in your environment variables

### Frontend Deployment (Vercel)

1. **Connect to Vercel**:
   - Sign up at [vercel.com](https://vercel.com)
   - Connect your GitHub repository
   - Select the `frontend` folder as the root directory

2. **Configure Environment Variables**:
   ```
   REACT_APP_API_URL=https://your-backend-url.onrender.com
   ```

3. **Build Settings**:
   - **Framework Preset**: Create React App
   - **Build Command**: `npm run build`
   - **Output Directory**: `build`

### Manual Deployment Steps

#### Backend (Render)
```bash
# 1. Push your code to GitHub
git add .
git commit -m "Deploy to production"
git push origin main

# 2. In Render dashboard:
# - Create new Web Service
# - Connect GitHub repository
# - Set build and start commands
# - Configure environment variables
```

#### Frontend (Vercel)
```bash
# 1. Install Vercel CLI
npm i -g vercel

# 2. Deploy from frontend directory
cd frontend
vercel

# 3. Set environment variables in Vercel dashboard
# REACT_APP_API_URL=https://your-backend-url.onrender.com
```

### Production URLs
- **Backend API**: `https://bookcrudapp-backend.onrender.com`
- **Frontend App**: `https://bookcrudapp-frontend.vercel.app`
- **API Documentation**: `https://bookcrudapp-backend.onrender.com/swagger`

### Environment Variables Reference

#### Backend (Render)
| Variable | Description | Example |
|----------|-------------|---------|
| `ASPNETCORE_ENVIRONMENT` | Environment setting | `Production` |
| `ASPNETCORE_URLS` | Server binding | `http://0.0.0.0:8080` |
| `DB_HOST` | Database host | `dpg-xxxxx-a.oregon-postgres.render.com` |
| `DB_PORT` | Database port | `5432` |
| `DB_NAME` | Database name | `bookcrudapp` |
| `DB_USER` | Database user | `bookcrudapp_user` |
| `DB_PASSWORD` | Database password | `auto-generated` |

#### Frontend (Vercel)
| Variable | Description | Example |
|----------|-------------|---------|
| `REACT_APP_API_URL` | Backend API URL | `https://bookcrudapp-backend.onrender.com` |

## Support
For support and questions, please open an issue in the GitHub repository.

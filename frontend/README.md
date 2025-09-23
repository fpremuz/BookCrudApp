# Book CRUD App - Frontend

A React TypeScript application for managing books with full CRUD operations.

## Features

- **BookList**: Display all books with Edit and Delete buttons
- **AddBook**: Form to create a new book (Title, Author, Pages)
- **EditBook**: Form to edit an existing book
- **Navigation**: Clean navigation bar with "Books" and "Add Book" links
- **API Integration**: All CRUD requests go to `http://localhost:5239/books`

## Tech Stack

- React 19.1.1
- TypeScript 4.9.5
- React Router DOM 7.9.1
- CSS3 for styling

## Getting Started

1. Install dependencies:
   ```bash
   npm install
   ```

2. Start the development server:
   ```bash
   npm start
   ```

3. Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

## API Endpoints

The app expects a backend API running on `http://localhost:5239/books` with the following endpoints:

- `GET /books` - Get all books
- `GET /books/:id` - Get a specific book
- `POST /books` - Create a new book
- `PUT /books/:id` - Update a book
- `DELETE /books/:id` - Delete a book

## Project Structure

```
src/
├── api/
│   └── books.ts          # API helper functions
├── components/
│   ├── BookList.tsx      # Book list component
│   ├── AddBook.tsx       # Add book form
│   └── EditBook.tsx      # Edit book form
├── types/
│   └── Book.ts           # TypeScript interfaces
├── App.tsx               # Main app component with routing
├── index.tsx             # App entry point
└── App.css               # Styling
```

## Available Scripts

- `npm start` - Runs the app in development mode
- `npm build` - Builds the app for production
- `npm test` - Launches the test runner
- `npm eject` - Ejects from Create React App (one-way operation)
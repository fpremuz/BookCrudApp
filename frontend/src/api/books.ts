import { Book, CreateBookRequest, UpdateBookRequest } from '../types/Book';

// Get API base URL from environment variable with fallback
const getApiBaseUrl = (): string => {
  const apiUrl = process.env.REACT_APP_API_URL;
  if (!apiUrl) {
    console.warn('REACT_APP_API_URL is not set. Using default localhost URL.');
    return 'http://localhost:5239';
  }
  return apiUrl;
};

const API_BASE_URL = `${getApiBaseUrl()}/books`;

export const booksApi = {
  // Get all books
  getAllBooks: async (): Promise<Book[]> => {
    const response = await fetch(API_BASE_URL);
    if (!response.ok) {
      throw new Error('Failed to fetch books');
    }
    return response.json();
  },

  // Get a single book by ID
  getBookById: async (id: number): Promise<Book> => {
    const response = await fetch(`${API_BASE_URL}/${id}`);
    if (!response.ok) {
      throw new Error('Failed to fetch book');
    }
    return response.json();
  },

  // Create a new book
  createBook: async (book: CreateBookRequest): Promise<Book> => {
    const response = await fetch(API_BASE_URL, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(book),
    });
    if (!response.ok) {
      throw new Error('Failed to create book');
    }
    return response.json();
  },

  // Update an existing book (backend returns 204 No Content)
  updateBook: async (book: UpdateBookRequest): Promise<void> => {
    const response = await fetch(`${API_BASE_URL}/${book.id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ title: book.title, author: book.author, pages: book.pages }),
    });
    if (!response.ok) {
      throw new Error('Failed to update book');
    }
    // No JSON to return on 204
  },

  // Delete a book
  deleteBook: async (id: number): Promise<void> => {
    const response = await fetch(`${API_BASE_URL}/${id}`, {
      method: 'DELETE',
    });
    if (!response.ok) {
      throw new Error('Failed to delete book');
    }
  },

  // AI-powered search
  searchBooks: async (query: string): Promise<Book[]> => {
    const response = await fetch(`${API_BASE_URL}/search?q=${encodeURIComponent(query)}`);
    if (!response.ok) {
      throw new Error('Failed to search books');
    }
    return response.json();
  },

  // Generate embeddings for all books
  generateEmbeddings: async (): Promise<void> => {
    const response = await fetch(`${API_BASE_URL}/generate-embeddings`, {
      method: 'POST',
    });
    if (!response.ok) {
      throw new Error('Failed to generate embeddings');
    }
  },
};

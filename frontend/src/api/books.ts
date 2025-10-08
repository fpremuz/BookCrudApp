import { Book, CreateBookRequest, UpdateBookRequest } from '../types/Book';

// API base URL from env var
const getApiBaseUrl = (): string => {
  const apiUrl = process.env.REACT_APP_API_URL;
  if (!apiUrl) {
    console.warn('REACT_APP_API_URL is not set. Using default localhost URL.');
    return 'http://localhost:8080';
  }
  // REMOVE trailing slash!
  return apiUrl.replace(/\/$/, '');
};

const API_BASE_URL = `${getApiBaseUrl()}/books`;

export const booksApi = {
  getAllBooks: async (): Promise<Book[]> => {
    const response = await fetch(API_BASE_URL);
    if (!response.ok) throw new Error('Failed to fetch books');
    return response.json();
  },

  getBookById: async (id: number): Promise<Book> => {
    const response = await fetch(`${API_BASE_URL}/${id}`);
    if (!response.ok) throw new Error('Failed to fetch book');
    return response.json();
  },

  createBook: async (book: CreateBookRequest): Promise<Book> => {
    const response = await fetch(API_BASE_URL, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(book),
    });
    if (!response.ok) {
      const text = await response.text();
      console.error('Backend response:', text);
      throw new Error('Failed to create book');
    }
    return response.json();
  },

  updateBook: async (book: UpdateBookRequest): Promise<void> => {
    const response = await fetch(`${API_BASE_URL}/${book.id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ title: book.title, author: book.author, pages: book.pages }),
    });
    if (!response.ok) throw new Error('Failed to update book');
  },

  deleteBook: async (id: number): Promise<void> => {
    const response = await fetch(`${API_BASE_URL}/${id}`, { method: 'DELETE' });
    if (!response.ok) throw new Error('Failed to delete book');
  },

  searchBooks: async (query: string): Promise<Book[]> => {
    const response = await fetch(`${API_BASE_URL}/search?q=${encodeURIComponent(query)}`);
    if (!response.ok) throw new Error('Failed to search books');
    return response.json();
  },

  generateEmbeddings: async (): Promise<void> => {
    const response = await fetch(`${API_BASE_URL}/generate-embeddings`, { method: 'POST' });
    if (!response.ok) throw new Error('Failed to generate embeddings');
  },
};
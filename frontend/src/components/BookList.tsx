import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Book } from '../types/Book';
import { booksApi } from '../api/books';

const BookList: React.FC = () => {
  const [books, setBooks] = useState<Book[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    fetchBooks();
  }, []);

  const fetchBooks = async () => {
    try {
      setLoading(true);
      const booksData = await booksApi.getAllBooks();
      setBooks(booksData);
      setError(null);
    } catch (err) {
      setError('Failed to fetch books');
      console.error('Error fetching books:', err);
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('Are you sure you want to delete this book?')) {
      try {
        await booksApi.deleteBook(id);
        setBooks(books.filter(book => book.id !== id));
      } catch (err) {
        setError('Failed to delete book');
        console.error('Error deleting book:', err);
      }
    }
  };

  if (loading) {
    return <div className="loading">Loading books...</div>;
  }

  if (error) {
    return (
      <div className="error">
        <p>{error}</p>
        <button onClick={fetchBooks}>Retry</button>
      </div>
    );
  }

  return (
    <div className="book-list">
      <h1>Books</h1>
      {books.length === 0 ? (
        <p>No books found. <Link to="/add-book">Add a book</Link> to get started.</p>
      ) : (
        <div className="books-grid">
          {books.map(book => (
            <div key={book.id} className="book-card">
              <h3>{book.title}</h3>
              <p><strong>Author:</strong> {book.author}</p>
              <p><strong>Pages:</strong> {book.pages}</p>
              <div className="book-actions">
                <Link to={`/edit-book/${book.id}`} className="btn btn-edit">
                  Edit
                </Link>
                <button 
                  onClick={() => handleDelete(book.id)}
                  className="btn btn-delete"
                >
                  Delete
                </button>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default BookList;

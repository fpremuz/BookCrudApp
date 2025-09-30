import React, { useState } from 'react';
import { Book } from '../types/Book';
import { booksApi } from '../api/books';

const SearchBooks: React.FC = () => {
  const [query, setQuery] = useState('');
  const [searchResults, setSearchResults] = useState<Book[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [hasSearched, setHasSearched] = useState(false);

  const handleSearch = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!query.trim()) return;

    try {
      setLoading(true);
      setError(null);
      const results = await booksApi.searchBooks(query.trim());
      setSearchResults(results);
      setHasSearched(true);
    } catch (err) {
      setError('Failed to search books');
      console.error('Error searching books:', err);
    } finally {
      setLoading(false);
    }
  };

  const handleGenerateEmbeddings = async () => {
    try {
      setLoading(true);
      setError(null);
      await booksApi.generateEmbeddings();
      alert('Embeddings generated successfully! You can now search for books.');
    } catch (err) {
      setError('Failed to generate embeddings');
      console.error('Error generating embeddings:', err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="search-books">
      <h1>AI-Powered Book Search</h1>
      
      <div className="search-section">
        <form onSubmit={handleSearch} className="search-form">
          <div className="search-input-group">
            <input
              type="text"
              value={query}
              onChange={(e) => setQuery(e.target.value)}
              placeholder="Search for books by content, genre, or topic..."
              className="search-input"
              disabled={loading}
            />
            <button 
              type="submit" 
              className="btn btn-search"
              disabled={loading || !query.trim()}
            >
              {loading ? 'Searching...' : 'Search'}
            </button>
          </div>
        </form>

        <div className="admin-section">
          <button 
            onClick={handleGenerateEmbeddings}
            className="btn btn-secondary"
            disabled={loading}
          >
            {loading ? 'Generating...' : 'Generate AI Embeddings'}
          </button>
          <p className="admin-note">
            Click this button to generate AI embeddings for all books. This enables AI-powered search.
          </p>
        </div>
      </div>

      {error && (
        <div className="error">
          <p>{error}</p>
        </div>
      )}

      {hasSearched && (
        <div className="search-results">
          <h2>Search Results</h2>
          {searchResults.length === 0 ? (
            <p>No books found matching your search. Try different keywords or generate embeddings first.</p>
          ) : (
            <div className="results-grid">
              {searchResults.map(book => (
                <div key={book.id} className="book-card search-result">
                  <h3>{book.title}</h3>
                  <p><strong>Author:</strong> {book.author}</p>
                  <p><strong>Pages:</strong> {book.pages}</p>
                  {book.summary && (
                    <div className="book-summary">
                      <strong>AI Summary:</strong>
                      <p>{book.summary}</p>
                    </div>
                  )}
                </div>
              ))}
            </div>
          )}
        </div>
      )}
    </div>
  );
};

export default SearchBooks;

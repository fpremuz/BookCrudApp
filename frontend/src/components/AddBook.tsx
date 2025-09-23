import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { CreateBookRequest } from '../types/Book';
import { booksApi } from '../api/books';

const AddBook: React.FC = () => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState<CreateBookRequest>({
    title: '',
    author: '',
    pages: 0,
  });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: name === 'pages' ? parseInt(value) || 0 : value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!formData.title.trim() || !formData.author.trim() || formData.pages <= 0) {
      setError('Please fill in all fields with valid values');
      return;
    }

    try {
      setLoading(true);
      setError(null);
      await booksApi.createBook(formData);
      navigate('/');
    } catch (err) {
      setError('Failed to create book');
      console.error('Error creating book:', err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="add-book">
      <h1>Add New Book</h1>
      <form onSubmit={handleSubmit} className="book-form">
        <div className="form-group">
          <label htmlFor="title">Title:</label>
          <input
            type="text"
            id="title"
            name="title"
            value={formData.title}
            onChange={handleChange}
            required
          />
        </div>
        
        <div className="form-group">
          <label htmlFor="author">Author:</label>
          <input
            type="text"
            id="author"
            name="author"
            value={formData.author}
            onChange={handleChange}
            required
          />
        </div>
        
        <div className="form-group">
          <label htmlFor="pages">Pages:</label>
          <input
            type="number"
            id="pages"
            name="pages"
            value={formData.pages}
            onChange={handleChange}
            min="1"
            required
          />
        </div>

        {error && <div className="error">{error}</div>}

        <div className="form-actions">
          <button type="submit" disabled={loading} className="btn btn-primary">
            {loading ? 'Creating...' : 'Create Book'}
          </button>
          <button 
            type="button" 
            onClick={() => navigate('/')} 
            className="btn btn-secondary"
          >
            Cancel
          </button>
        </div>
      </form>
    </div>
  );
};

export default AddBook;

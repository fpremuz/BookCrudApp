import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import BookList from './components/BookList';
import AddBook from './components/AddBook';
import EditBook from './components/EditBook';
import SearchBooks from './components/SearchBooks';
import './App.css';

const App: React.FC = () => {
  return (
    <Router>
      <div className="App">
        <nav className="navbar">
          <div className="nav-container">
            <Link to="/" className="nav-brand">
              Book Manager
            </Link>
            <div className="nav-links">
              <Link to="/" className="nav-link">
                Books
              </Link>
              <Link to="/search" className="nav-link">
                AI Search
              </Link>
              <Link to="/add-book" className="nav-link">
                Add Book
              </Link>
            </div>
          </div>
        </nav>

        <main className="main-content">
          <Routes>
            <Route path="/" element={<BookList />} />
            <Route path="/search" element={<SearchBooks />} />
            <Route path="/add-book" element={<AddBook />} />
            <Route path="/edit-book/:id" element={<EditBook />} />
          </Routes>
        </main>
      </div>
    </Router>
  );
};

export default App;

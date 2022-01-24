import React from 'react';
import './App.css';
import { TodosTable } from './components/molecules/tables/TodoTable/TodosTable';
import { NavBar } from './components/molecules/NavBar/NavBar';
import { BrowserRouter, Route, Routes } from 'react-router-dom';

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <NavBar role="FRONTMAN" />
        <Routes>
          <Route path="/" element={TodosTable} />
          <Route path="/users" element={<div></div>} />
        </Routes>
    </BrowserRouter>
    </div>
  );
}

export default App;

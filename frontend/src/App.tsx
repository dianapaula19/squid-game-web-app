import React from 'react';
import logo from './logo.svg';
import { Counter } from './features/counter/Counter';
import './App.css';
import { AdminLoginForm } from './components/molecules/AdminLoginForm/AdminLoginForm';

function App() {
  return (
    <div className="App">
      <AdminLoginForm />
    </div>
  );
}

export default App;

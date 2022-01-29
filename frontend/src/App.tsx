import React from 'react';
import { AuthPage, AuthOption } from './components/pages/AuthPage/AuthPage';
import { NavBar } from './components/molecules/NavBar/NavBar';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { GamePage } from './components/pages/GamePage/GamePage';
import { Role } from './Utils';

function App() {
  return (
    <BrowserRouter>
      <NavBar role={Role.player} />
      <Routes>
        <Route path="/login" element={<AuthPage option={AuthOption.login}/>}/>
        <Route path="/register" element={<AuthPage option={AuthOption.register}/>}/>
        <Route path="/game" element={<GamePage />}/>
      </Routes>

    </BrowserRouter>
  );
}

export default App;

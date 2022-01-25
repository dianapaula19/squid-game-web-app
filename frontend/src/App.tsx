import React from 'react';
import { AuthPage, AuthOption } from './components/components/pages/AuthPage/AuthPage';
import { NavBar } from './components/components/molecules/NavBar/NavBar';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { GamePage } from './components/components/pages/GamePage/GamePage';

export enum Role {
  player = 'Player',
  guard = 'Guard',
  frontman = 'FrontMan',
  undefined = 'Undefined'
}


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

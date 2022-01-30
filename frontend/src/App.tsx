import React from 'react';
import { AuthPage, AuthOption } from './components/pages/AuthPage/AuthPage';
import { NavBar } from './components/molecules/NavBar/NavBar';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { GamePage } from './components/pages/GamePage/GamePage';
import { GuardRole, Role } from './Utils';
import { ProfilePage } from './components/pages/ProfilePage/ProfilePage';
import { useAppSelector } from './app/hooks';
import { email } from './features/auth/authSlice';

function App() {
  const userEmail = useAppSelector(email);
  const profilePath = `/profile/:${userEmail}`;
  return (
    <BrowserRouter>
      <NavBar role={Role.player} />
      <Routes>
        <Route path="/login" element={<AuthPage option={AuthOption.login}/>}/>
        <Route path="/register" element={<AuthPage option={AuthOption.register}/>}/>
        <Route path="/game" element={<GamePage />}/>
        <Route path={profilePath} element={<ProfilePage role={Role.guard} guardRole={GuardRole.worker}/>}/>
      </Routes>
    </BrowserRouter>
  );
}

export default App;

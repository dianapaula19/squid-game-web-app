import React from 'react';
import { AuthPage, AuthOption } from './components/pages/AuthPage/AuthPage';
import { NavBar } from './components/molecules/NavBar/NavBar';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { ProfilePage } from './components/pages/ProfilePage/ProfilePage';
import { useAppSelector } from './app/hooks';
import { authState} from './features/auth/authSlice';
import { Role } from './Utils';

const App = () => {
  const { role } = useAppSelector(authState);

  return (
    <BrowserRouter>
      <NavBar role={role} />
      <Routes>
        {
          role === Role.undefined ? (
            <>
              <Route path="/login" element={<AuthPage option={AuthOption.login}/>}/>
              <Route path="/register" element={<AuthPage option={AuthOption.register}/>}/>
            </>
          ) : (
            <>
              <Route path={`/profile`} element={<ProfilePage role={role}/>}/>
            </>
          )
        }
      </Routes>
    </BrowserRouter>
  );
}

export default App;

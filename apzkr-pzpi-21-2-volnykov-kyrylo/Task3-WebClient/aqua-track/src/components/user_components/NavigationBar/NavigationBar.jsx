import React from 'react';
import { AppBar, Toolbar, Typography, Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import LanguageSwitcher from '../../LanguageSwitcher/LanguageSwitcher';
import NavButtons from '../NavButtons/NavButtons';

const NavigationBar = ({ title }) => {
  const navigate = useNavigate();
  const { t } = useTranslation();
  const handleLogout = () => {
    localStorage.removeItem("role");
    localStorage.removeItem("userId");

    navigate('/user/login');
  };

  const role = localStorage.getItem("role");
  console.log("Role " + role);
  console.log(typeof role);

  return (
    <AppBar position="static">
      <Toolbar>
        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
          {title}
        </Typography>
        {(+role === 0) && (<NavButtons />)}
        <LanguageSwitcher />
        <Button color="inherit" onClick={handleLogout}>{t('navigationBar.logout')}</Button>
      </Toolbar>
    </AppBar>
  );
};

export default NavigationBar;

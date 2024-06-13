import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { Link, useNavigate } from 'react-router-dom';
import { TextField, Button, Typography, Container, Box, Alert } from '@mui/material';
import { getCurrentUserInfo, login } from '../../../api/users/users';
import LanguageSwitcher from '../../LanguageSwitcher/LanguageSwitcher';

const Login = () => {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState(null);

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      const userInfo = await login(email, password);
      
      localStorage.setItem("role", userInfo.role);
      localStorage.setItem("userId", userInfo.userId);

      if (userInfo.role === 1) {
        // Redirect admin to admin route
        navigate('/admin/panel');
      } else {
        // Redirect regular user to user route
        navigate('/user/aquarium');
      }
    } catch (error) {
      console.error(error.message);
    }
  };

  return (
    <Container maxWidth="sm">
      <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', mt: 5 }}>
        <Box sx={{ alignSelf: 'flex-end'}}>
          <LanguageSwitcher />
        </Box>
        <Typography variant="h4" component="h1" gutterBottom>{t('login.title')}</Typography>
        {error && <Alert severity="error">{error}</Alert>}
        <Box component="form" onSubmit={handleSubmit} sx={{ mt: 1 , width: '100%'}}>
          <TextField
            label={t('login.email')}
            type="email"
            fullWidth
            margin="normal"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
          <TextField
            label={t('login.password')}
            type="password"
            fullWidth
            margin="normal"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
          <Button type="submit" fullWidth variant="contained" color="primary" sx={{ mt: 3, mb: 2 }}>
            {t('login.submit')}
          </Button>
          <Typography variant="body2">
            {t('login.noAccount')} <Link to="/user/sign-up">{t('login.signup')}</Link>
          </Typography>
        </Box>
      </Box>
    </Container>
  );
};

export default Login;

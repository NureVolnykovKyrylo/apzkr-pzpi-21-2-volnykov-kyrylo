import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { Link, useNavigate } from 'react-router-dom';
import { TextField, Button, Typography, Container, Box, Alert } from '@mui/material';
import { register } from '../../../api/users/users';
import LanguageSwitcher from '../../LanguageSwitcher/LanguageSwitcher';

const SignUp = () => {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const [userData, setUserData] = useState({
    email: '',
    password: '',
    confirmPassword: '',
    // Add any other fields required by your UserRegisterViewModel
  });
  const [error, setError] = useState(null);

  const handleChange = (event) => {
    const { name, value } = event.target;
    setUserData({ ...userData, [name]: value });
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    if (userData.password !== userData.confirmPassword) {
      setError(t('signup.passwordMismatch'));
      return;
    }
    try {
      await register(userData);
      navigate('/login'); // Redirect to login page after successful registration
    } catch (err) {
      setError(err.message);
    }
  };

  return (
    <Container maxWidth="sm">
      <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', mt: 5 }}>
        <Box sx={{ alignSelf: 'flex-end' }}>
          <LanguageSwitcher />
        </Box>
        <Typography variant="h4" component="h1" gutterBottom>{t('signup.title')}</Typography>
        {error && <Alert severity="error">{error}</Alert>}
        <Box component="form" onSubmit={handleSubmit} sx={{ mt: 1, width: '100%' }}>
          <TextField
            label={t('signup.email')}
            type="email"
            name="email"
            fullWidth
            margin="normal"
            value={userData.email}
            onChange={handleChange}
            required
          />
          <TextField
            label={t('signup.password')}
            type="password"
            name="password"
            fullWidth
            margin="normal"
            value={userData.password}
            onChange={handleChange}
            required
          />
          <TextField
            label={t('signup.confirmPassword')}
            type="password"
            name="confirmPassword"
            fullWidth
            margin="normal"
            value={userData.confirmPassword}
            onChange={handleChange}
            required
          />
          {/* Add other input fields as required */}
          <Button type="submit" fullWidth variant="contained" color="primary" sx={{ mt: 3, mb: 2 }}>
            {t('signup.submit')}
          </Button>
          <Typography variant="body2">
            {t('signup.haveAccount')} <Link to="/user/login">{t('signup.login')}</Link>
          </Typography>
        </Box>
      </Box>
    </Container>
  );
};

export default SignUp;

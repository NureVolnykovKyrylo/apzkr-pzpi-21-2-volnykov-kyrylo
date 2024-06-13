import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Typography, Button, TextField, Divider } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { getUserById, updateUser } from '../../../api/users/users';
import LanguageSwitcher from '../../LanguageSwitcher/LanguageSwitcher';
import NavigationBar from '../NavigationBar/NavigationBar';

const UserUpdateForm = () => {
  const { userId } = useParams();
  const { t } = useTranslation();
  const navigate = useNavigate();
  const [user, setUser] = useState(null);
  const [formData, setFormData] = useState({
    password: '',
    email: '',
    phoneNumber: '',
    firstName: '',
    lastName: ''
  });

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const userData = await getUserById(userId);
        setUser(userData);
        setFormData({
          password: '',
          email: userData.email,
          phoneNumber: userData.phoneNumber,
          firstName: userData.firstName,
          lastName: userData.lastName
        });
      } catch (error) {
        console.error(error.message);
      }
    };
    fetchUser();
  }, [userId]);

  const handleUpdate = async () => {
    try {
      await updateUser({ ...user, ...formData });
      navigate('/admin/panel');
    } catch (error) {
      console.error(error.message);
    }
  };

  if (!user) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      <NavigationBar title = {t('user.updateUser')}></NavigationBar>
      <Divider />
      <TextField
        label={t('user.password')}
        fullWidth
        margin="normal"
        value={formData.password}
        onChange={(e) => setFormData({ ...formData, password: e.target.value })}
      />
      <TextField
        label={t('user.email')}
        fullWidth
        margin="normal"
        value={formData.email}
        onChange={(e) => setFormData({ ...formData, email: e.target.value })}
      />
      <TextField
        label={t('user.phoneNumber')}
        fullWidth
        margin="normal"
        value={formData.phoneNumber}
        onChange={(e) => setFormData({ ...formData, phoneNumber: e.target.value })}
      />
      <TextField
        label={t('user.firstName')}
        fullWidth
        margin="normal"
        value={formData.firstName}
        onChange={(e) => setFormData({ ...formData, firstName: e.target.value })}
      />
      <TextField
        label={t('user.lastName')}
        fullWidth
        margin="normal"
        value={formData.lastName}
        onChange={(e) => setFormData({ ...formData, lastName: e.target.value })}
      />
      <Button variant="contained" color="primary" onClick={handleUpdate}>
        {t('user.update')}
      </Button>
    </div>
  );
};

export default UserUpdateForm;

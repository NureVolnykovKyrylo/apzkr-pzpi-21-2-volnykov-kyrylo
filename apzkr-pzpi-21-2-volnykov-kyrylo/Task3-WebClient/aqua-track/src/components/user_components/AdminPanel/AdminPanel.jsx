import React, { useState } from 'react';
import { AppBar, Toolbar, Typography, Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import AdminUser from '../AdminUser/AdminUser';
import NavigationBar from '../NavigationBar/NavigationBar';
import { useTranslation } from 'react-i18next';

const AdminPanel = () => {
  const navigate = useNavigate();
  const { t } = useTranslation();
  const handleUserSelect = (userId) => {
    navigate(`/admin/aquarium/${userId}`);
  };

  const handleLogout = () => {
    // Clear local storage
    localStorage.removeItem("role");
    localStorage.removeItem("userId");
    
    // Redirect to login page
    navigate('/user/login');
  };

  return (
    <div>
      <NavigationBar title = {t('adminPanel.title')}></NavigationBar>
      <AdminUser onUserSelect={handleUserSelect} />
    </div>
  );
};

export default AdminPanel;

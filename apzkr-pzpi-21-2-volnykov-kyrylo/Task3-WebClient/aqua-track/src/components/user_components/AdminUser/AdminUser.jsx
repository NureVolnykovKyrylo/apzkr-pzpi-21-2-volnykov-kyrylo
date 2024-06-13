import React, { useState, useEffect } from 'react';
import { Typography, Button, Divider, List, ListItem, ListItemText } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { getAllUsers, deleteUser } from '../../../api/users/users';
import LanguageSwitcher from '../../LanguageSwitcher/LanguageSwitcher';
import { useNavigate } from 'react-router-dom';
import NavigationBar from '../NavigationBar/NavigationBar';

const AdminUser = ({ onUserSelect }) => {
  const { t } = useTranslation();
  const [users, setUsers] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const allUsers = await getAllUsers();
        const nonAdminUsers = allUsers.filter(user => user.role !== 1);
        setUsers(nonAdminUsers);
      } catch (error) {
        console.error(error.message);
      }
    };
    fetchUsers();
  }, []);

  const handleDelete = async (id) => {
    try {
      await deleteUser(id);
      setUsers(users.filter(user => user.id !== id));
    } catch (error) {
      console.error(error.message);
    }
  };

  return (
    <div>
      <Divider />
      <Typography variant="h5">{t('user.selectUser')}</Typography>
      <List sx={{maxWidth: "600px", marginInline: "auto", padding: "20px"}}>
        {users.map(user => (
          <ListItem key={user.userId}>
            <ListItemText primary={user.email} />
            <Button
              variant="contained"
              color="primary"
              onClick={() => navigate(`/admin/update/${user.userId}`)}
            >
              {t('user.update')}
            </Button>
            <Button variant="contained" color="secondary" onClick={() => handleDelete(user.userId)}>
              {t('user.delete')}
            </Button>
            <Button variant="contained" color="warning" onClick={() => onUserSelect(user.userId)}>
              {t('user.aquariums')}
            </Button>
          </ListItem>
        ))}
      </List>
    </div>
  );
};

export default AdminUser;

import React, { useState, useEffect } from 'react';
import { Typography, Button, Divider, List, ListItem, ListItemText, ListItemSecondaryAction, TextField, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { Link, useParams } from 'react-router-dom';
import { deleteInhabitantForCurrentUser, getInhabitantsForAquarium } from '../../../api/inhabitants/inhabitants';
import NavigationBar from '../../user_components/NavigationBar/NavigationBar';

const Inhabitant = () => {
  const { t } = useTranslation();
  const { aquariumId } = useParams();
  const [inhabitants, setInhabitants] = useState([]);

  useEffect(() => {
    const fetchInhabitants = async () => {
      try {
        const data = await getInhabitantsForAquarium(aquariumId);
        setInhabitants(data);
      } catch (error) {
        console.error(error.message);
      }
    };
    fetchInhabitants();
  }, [aquariumId]);

  const handleDelete = async (id) => {
    try {
      await deleteInhabitantForCurrentUser(id);
      setInhabitants(inhabitants.filter(inhabitant => inhabitant.inhabitantId !== id));
    } catch (error) {
      console.error(error.message);
    }
  };

  return (
    <div>
      <NavigationBar title={t('inhabitant.inhabitants')} />
      <Divider />

      <Button
        variant="contained"
        color="primary"
        component={Link}
        to={`/user/inhabitant/add/${aquariumId}`}
      >
        {t('inhabitant.add')}
      </Button>

      <Divider />
      <List sx={{maxWidth: "600px", marginInline: "auto", padding: "20px"}}>
        {inhabitants.map((inhabitant) => (
          <ListItem key={inhabitant.inhabitantId}>
            <ListItemText
              primary={inhabitant.name}
              secondary={`${t('inhabitant.species')}: ${inhabitant.species}`}
            />
            <ListItemSecondaryAction>
              <Button
                variant="contained"
                color="primary"
                component={Link}
                to={`/user/inhabitant/${aquariumId}/update/${inhabitant.inhabitantId}`}
              >
                {t('inhabitant.update')}
              </Button>
              <Button
                variant="contained"
                color="secondary"
                onClick={() => handleDelete(inhabitant.inhabitantId)}
                sx={{marginInline: "4px"}}
              >
                {t('inhabitant.delete')}
              </Button>
              <Button
                variant="contained"
                color="warning"
                component={Link}
                to={`/user/inhabitant/view/${inhabitant.inhabitantId}`}
              >
                {t('inhabitant.view')}
              </Button>
            </ListItemSecondaryAction>
          </ListItem>
        ))}
      </List>
    </div>
  );
};

export default Inhabitant;

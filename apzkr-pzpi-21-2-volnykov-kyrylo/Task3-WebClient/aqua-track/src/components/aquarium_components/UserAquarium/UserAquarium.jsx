import React, { useState, useEffect } from 'react';
import { Typography, Button, Divider, List, ListItem, ListItemText, ListItemSecondaryAction } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { getAquariumsForUser, deleteAquariumForUser } from '../../../api/aquariums/aquariums';
import { Link, useNavigate, useParams, useSearchParams } from 'react-router-dom';
import NavigationBar from '../../user_components/NavigationBar/NavigationBar';

const UserAquarium = ({ selectedUserId }) => {
  const { t } = useTranslation();
  const [aquariums, setAquariums] = useState([]);
  const navigate = useNavigate();
  const userId = localStorage.getItem("userId");

  useEffect(() => {
    if (userId) {
      const fetchAquariums = async () => {
        try {
          const data = await getAquariumsForUser(userId);
          setAquariums(data);
        } catch (error) {
          console.error(error.message);
        }
      };
      fetchAquariums();
    }
  }, [userId]);

  const handleDelete = async (id) => {
    try {
      await deleteAquariumForUser(id, userId);
      setAquariums(aquariums.filter(aquarium => aquarium.id !== id));
    } catch (error) {
      console.error(error.message);
    }
  };

  return (
    <div>
      <NavigationBar title = {t('aquarium.aquariumAdministration')}></NavigationBar>
      <Divider />
      
      <List sx={{maxWidth: "600px", marginInline: "auto", padding: "20px"}}>
        {aquariums.map((aquarium) => (
          <ListItem key={aquarium.aquariumId}>
            <ListItemText
              secondary={`${t('aquarium.type')}: ${aquarium.aquariumType}`}
            />
            <ListItemSecondaryAction>
              <Button
                variant="contained"
                color="warning"
                component={Link}
                to={`/general/aquarium/view/${aquarium.aquariumId}`}
              >
                {t('aquarium.view')}
              </Button>
              <Button
                variant="contained"
                color="info"
                component={Link}
                to={`/user/inhabitant/${aquarium.aquariumId}`}
              >
                {t('aquarium.inhabitants')}
              </Button>
            </ListItemSecondaryAction>
          </ListItem>
        ))}
      </List>
    </div>
  );
};

export default UserAquarium;

import React, { useState, useEffect } from 'react';
import { Typography, Button, Divider, List, ListItem, ListItemText, ListItemSecondaryAction } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { getAquariumsForUser, deleteAquariumForUser } from '../../../api/aquariums/aquariums';
import { Link, useNavigate, useParams, useSearchParams } from 'react-router-dom';
import LanguageSwitcher from '../../LanguageSwitcher/LanguageSwitcher';
import NavigationBar from '../../user_components/NavigationBar/NavigationBar';

const AdminAquarium = ({ selectedUserId }) => {
  const { t } = useTranslation();
  const [aquariums, setAquariums] = useState([]);
  const navigate = useNavigate();
  const { userId } = useParams();

  useEffect(() => {
    if (userId) {
      const fetchAquariums = async () => {
        try {
          const data = await getAquariumsForUser(userId);
          console.log(data);
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
      <Button
        variant="contained"
        color="primary"
        component={Link}
        to={`/admin/aquarium/add/${userId}`}
      >
        {t('aquarium.addAquarium')}
      </Button>
      <Divider />
      <List sx={{maxWidth: "600px", marginInline: "auto", padding: "20px"}}>
        {aquariums.map((aquarium) => (
          <ListItem key={aquarium.aquariumId}>
            <ListItemText
              primary={aquarium.name}
              secondary={`${t('aquarium.type')}: ${aquarium.aquariumType}`}
            />
            <ListItemSecondaryAction>
              <Button
                variant="contained"
                color="primary"
                component={Link}
                to={`/admin/aquarium/${userId}/update/${aquarium.aquariumId}`}
              >
                {t('aquarium.update')}
              </Button>
              <Button
                variant="contained"
                color="secondary"
                onClick={() => handleDelete(aquarium.aquariumId)}
              >
                {t('aquarium.delete')}
              </Button>
              <Button
                variant="contained"
                color="warning"
                component={Link}
                to={`/general/aquarium/view/${aquarium.aquariumId}`}
              >
                {t('aquarium.view')}
              </Button>
            </ListItemSecondaryAction>
          </ListItem>
        ))}
      </List>
    </div>
  );
};

export default AdminAquarium;

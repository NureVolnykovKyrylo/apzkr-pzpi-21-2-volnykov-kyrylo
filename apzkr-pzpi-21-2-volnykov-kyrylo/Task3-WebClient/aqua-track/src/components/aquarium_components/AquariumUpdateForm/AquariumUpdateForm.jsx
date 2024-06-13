import React, { useState, useEffect } from 'react';
import { TextField, Button, Typography, Divider } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { updateAquariumForUser, getAquariumById } from '../../../api/aquariums/aquariums';
import { useParams, useNavigate } from 'react-router-dom';
import LanguageSwitcher from '../../LanguageSwitcher/LanguageSwitcher';
import NavigationBar from '../../user_components/NavigationBar/NavigationBar';

const AquariumUpdateForm = () => {
  const { t } = useTranslation();
  const { userId, aquariumId } = useParams();
  const navigate = useNavigate();
  const [aquarium, setAquarium] = useState(null);
  const [updatedAquarium, setUpdatedAquarium] = useState({
    aquariumType: '',
    acidity: 0,
    waterLevel: 0,
    temperature: 0,
    lighting: 0,
  });

  useEffect(() => {
    const fetchAquarium = async () => {
      try {
        const data = await getAquariumById(aquariumId);
        setAquarium(data);
        setUpdatedAquarium(data);
      } catch (error) {
        console.error(error.message);
      }
    };
    fetchAquarium();
  }, [aquariumId]);

  const handleChange = (event) => {
    const { name, value } = event.target;
    setUpdatedAquarium({ ...updatedAquarium, [name]: value });
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      await updateAquariumForUser(aquariumId, updatedAquarium, userId);
      navigate(`/admin/aquarium/${userId}`);
    } catch (error) {
      console.error(error.message);
    }
  };

  if (!aquarium) {
    return <div>{t('loading')}</div>;
  }

  return (
    <form onSubmit={handleSubmit}>
      <NavigationBar title = {t('aquarium.updateAquarium')}></NavigationBar>
      <Divider />
      <TextField
        name="aquariumType"
        label={t('aquarium.aquariumType')}
        fullWidth
        margin="normal"
        value={updatedAquarium.aquariumType}
        onChange={handleChange}
        required
      />
      <TextField
        name="acidity"
        label={t('aquarium.acidity')}
        fullWidth
        margin="normal"
        type="number"
        value={updatedAquarium.acidity}
        onChange={handleChange}
        required
      />
      <TextField
        name="waterLevel"
        label={t('aquarium.waterLevel')}
        fullWidth
        margin="normal"
        type="number"
        value={updatedAquarium.waterLevel}
        onChange={handleChange}
        required
      />
      <TextField
        name="temperature"
        label={t('aquarium.temperature')}
        fullWidth
        margin="normal"
        type="number"
        value={updatedAquarium.temperature}
        onChange={handleChange}
        required
      />
      <TextField
        name="lighting"
        label={t('aquarium.lighting')}
        fullWidth
        margin="normal"
        type="number"
        value={updatedAquarium.lighting}
        onChange={handleChange}
        required
      />
      <Button type="submit" variant="contained" color="primary">{t('aquarium.update')}</Button>
    </form>
  );
};

export default AquariumUpdateForm;

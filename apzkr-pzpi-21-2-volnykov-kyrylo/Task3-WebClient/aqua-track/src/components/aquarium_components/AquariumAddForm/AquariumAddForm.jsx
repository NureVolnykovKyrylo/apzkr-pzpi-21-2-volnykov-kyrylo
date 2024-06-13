import React, { useState } from 'react';
import { TextField, Button, Typography, Divider } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { addAquariumForUser } from '../../../api/aquariums/aquariums';
import { useNavigate, useParams } from 'react-router-dom';
import LanguageSwitcher from '../../LanguageSwitcher/LanguageSwitcher';
import NavigationBar from '../../user_components/NavigationBar/NavigationBar';

const AquariumAddForm = () => {
  const { t } = useTranslation();
  const { userId } = useParams();
  const navigate = useNavigate();
  const [aquarium, setAquarium] = useState({
    aquariumType: '',
    acidity: 0,
    waterLevel: 0,
    temperature: 0,
    lighting: 0,
  });

  const handleChange = (event) => {
    const { name, value } = event.target;
    setAquarium({ ...aquarium, [name]: value });
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      const addedAquarium = await addAquariumForUser(aquarium, userId);
      navigate(`/admin/aquarium/${userId}`);
    } catch (error) {
      console.error(error.message);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <NavigationBar title = {t('aquarium.addAquarium')}></NavigationBar>
      <Divider />
      <TextField
        name="aquariumType"
        label={t('aquarium.aquariumType')}
        fullWidth
        margin="normal"
        value={aquarium.aquariumType}
        onChange={handleChange}
        required
      />
      <TextField
        name="acidity"
        label={t('aquarium.acidity')}
        fullWidth
        margin="normal"
        type="number"
        value={aquarium.acidity}
        onChange={handleChange}
        required
      />
      <TextField
        name="waterLevel"
        label={t('aquarium.waterLevel')}
        fullWidth
        margin="normal"
        type="number"
        value={aquarium.waterLevel}
        onChange={handleChange}
        required
      />
      <TextField
        name="temperature"
        label={t('aquarium.temperature')}
        fullWidth
        margin="normal"
        type="number"
        value={aquarium.temperature}
        onChange={handleChange}
        required
      />
      <TextField
        name="lighting"
        label={t('aquarium.lighting')}
        fullWidth
        margin="normal"
        type="number"
        value={aquarium.lighting}
        onChange={handleChange}
        required
      />
      <Button type="submit" variant="contained" color="primary">{t('aquarium.add')}</Button>
    </form>
  );
};

export default AquariumAddForm;

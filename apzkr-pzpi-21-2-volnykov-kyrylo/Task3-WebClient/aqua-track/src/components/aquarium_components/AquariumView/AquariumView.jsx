import React, { useState, useEffect } from 'react';
import { Typography, Divider } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { getAquariumById } from '../../../api/aquariums/aquariums';
import { useParams } from 'react-router-dom';
import LanguageSwitcher from '../../LanguageSwitcher/LanguageSwitcher';
import NavigationBar from '../../user_components/NavigationBar/NavigationBar';

const AquariumView = () => {
  const { t } = useTranslation();
  const { userId, aquariumId } = useParams();
  const [aquarium, setAquarium] = useState(null);

  useEffect(() => {
    const fetchAquarium = async () => {
      try {
        const data = await getAquariumById(aquariumId);
        setAquarium(data);
      } catch (error) {
        console.error(error.message);
      }
    };
    fetchAquarium();
  }, [aquariumId]);

  if (!aquarium) {
    return <div>{t('loading')}</div>;
  }

  return (
    <div>
      <NavigationBar title = {t('aquarium.aquariumDetails')}></NavigationBar>
      <Divider />
      <Typography variant="body1">
        <strong>{t('aquarium.aquariumType')}:</strong> {aquarium.aquariumType}
      </Typography>
      <Typography variant="body1">
        <strong>{t('aquarium.acidity')}:</strong> {aquarium.acidity}
      </Typography>
      <Typography variant="body1">
        <strong>{t('aquarium.waterLevel')}:</strong> {aquarium.waterLevel}
      </Typography>
      <Typography variant="body1">
        <strong>{t('aquarium.temperature')}:</strong> {aquarium.temperature}
      </Typography>
      <Typography variant="body1">
        <strong>{t('aquarium.lighting')}:</strong> {aquarium.lighting}
      </Typography>
      {/* Add other fields as needed */}
    </div>
  );
};

export default AquariumView;

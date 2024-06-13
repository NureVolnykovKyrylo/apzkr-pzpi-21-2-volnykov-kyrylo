import React, { useState, useEffect } from 'react';
import { Typography, Divider } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { getInhabitantById } from '../../../api/inhabitants/inhabitants';
import { useParams } from 'react-router-dom';
import LanguageSwitcher from '../../LanguageSwitcher/LanguageSwitcher';
import NavigationBar from '../../user_components/NavigationBar/NavigationBar';

const InhabitantView = () => {
  const { t } = useTranslation();
  const { userId, aquariumId, inhabitantId } = useParams();
  const [inhabitant, setInhabitant] = useState(null);

  useEffect(() => {
    const fetchInhabitant = async () => {
      try {
        const data = await getInhabitantById(inhabitantId);
        setInhabitant(data);
      } catch (error) {
        console.error(error.message);
      }
    };
    fetchInhabitant();
  }, [inhabitantId]);

  if (!inhabitant) {
    return <div>{t('loading')}</div>;
  }

  return (
    <div>
      <NavigationBar title={t('inhabitant.inhabitantDetails')} />
      <Divider />
      <Typography variant="body1">
        <strong>{t('inhabitant.species')}:</strong> {inhabitant.species}
      </Typography>
      <Typography variant="body1">
        <strong>{t('inhabitant.name')}:</strong> {inhabitant.name}
      </Typography>
      <Typography variant="body1">
        <strong>{t('inhabitant.userNotes')}:</strong> {inhabitant.userNotes}
      </Typography>
      <Typography variant="body1">
        <strong>{t('inhabitant.behavior')}:</strong> {inhabitant.behavior}
      </Typography>
      <Typography variant="body1">
        <strong>{t('inhabitant.condition')}:</strong> {inhabitant.condition}
      </Typography>
      {/* Add other fields as needed */}
    </div>
  );
};

export default InhabitantView;

import React, { useState, useEffect } from 'react';
import { TextField, Button, Typography, Divider } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { getInhabitantById, updateInhabitantForCurrentUser} from '../../../api/inhabitants/inhabitants';
import { useParams, useNavigate } from 'react-router-dom';
import LanguageSwitcher from '../../LanguageSwitcher/LanguageSwitcher';
import NavigationBar from '../../user_components/NavigationBar/NavigationBar';

const InhabitantUpdateForm = () => {
  const { t } = useTranslation();
  const { userId, aquariumId, inhabitantId } = useParams();
  const navigate = useNavigate();
  const [inhabitant, setInhabitant] = useState(null);
  const [updatedInhabitant, setUpdatedInhabitant] = useState({
    species: '',
    name: '',
    userNotes: '',
    behavior: '',
    condition: '',
  });

  useEffect(() => {
    const fetchInhabitant = async () => {
      try {
        const data = await getInhabitantById(inhabitantId);
        setInhabitant(data);
        setUpdatedInhabitant(data);
      } catch (error) {
        console.error(error.message);
      }
    };
    fetchInhabitant();
  }, [inhabitantId]);

  const handleChange = (event) => {
    const { name, value } = event.target;
    setUpdatedInhabitant({ ...updatedInhabitant, [name]: value });
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      await updateInhabitantForCurrentUser(inhabitantId, updatedInhabitant);
      navigate(`/user/inhabitant/${aquariumId}`);
    } catch (error) {
      console.error(error.message);
    }
  };

  if (!inhabitant) {
    return <div>{t('loading')}</div>;
  }

  return (
    <form onSubmit={handleSubmit}>
      <NavigationBar title={t('inhabitant.updateInhabitant')} />
      <Divider />
      <TextField
        name="species"
        label={t('inhabitant.species')}
        fullWidth
        margin="normal"
        value={updatedInhabitant.species}
        onChange={handleChange}
        required
      />
      <TextField
        name="name"
        label={t('inhabitant.name')}
        fullWidth
        margin="normal"
        value={updatedInhabitant.name}
        onChange={handleChange}
        required
      />
      <TextField
        name="userNotes"
        label={t('inhabitant.userNotes')}
        fullWidth
        margin="normal"
        value={updatedInhabitant.userNotes}
        onChange={handleChange}
        required
      />
      <TextField
        name="behavior"
        label={t('inhabitant.behavior')}
        fullWidth
        margin="normal"
        value={updatedInhabitant.behavior}
        onChange={handleChange}
        required
      />
      <TextField
        name="condition"
        label={t('inhabitant.condition')}
        fullWidth
        margin="normal"
        value={updatedInhabitant.condition}
        onChange={handleChange}
        required
      />
      <Button type="submit" variant="contained" color="primary">
        {t('inhabitant.update')}
      </Button>
    </form>
  );
};

export default InhabitantUpdateForm;

import React, { useState } from 'react';
import { TextField, Button, Typography, Divider } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { addInhabitantForCurrentUser } from '../../../api/inhabitants/inhabitants';
import { useNavigate, useParams } from 'react-router-dom';
import LanguageSwitcher from '../../LanguageSwitcher/LanguageSwitcher';
import NavigationBar from '../../user_components/NavigationBar/NavigationBar';

const InhabitantAddForm = () => {
  const { t } = useTranslation();
  const { userId, aquariumId } = useParams();
  console.log(aquariumId);
  const navigate = useNavigate();
  const [inhabitant, setInhabitant] = useState({
    aquariumId: aquariumId,
    species: '',
    name: '',
    userNotes: '',
    behavior: '',
    condition: '',
  });

  const handleChange = (event) => {
    const { name, value } = event.target;
    setInhabitant({ ...inhabitant, [name]: value });
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      
      const addedInhabitant = await addInhabitantForCurrentUser(inhabitant);
      navigate(`/user/inhabitant/${aquariumId}`);
    } catch (error) {
      console.error(error.message);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <NavigationBar title={t('inhabitant.addInhabitant')} />
      <Divider />
      <TextField
        name="species"
        label={t('inhabitant.species')}
        fullWidth
        margin="normal"
        value={inhabitant.species}
        onChange={handleChange}
        required
      />
      <TextField
        name="name"
        label={t('inhabitant.name')}
        fullWidth
        margin="normal"
        value={inhabitant.name}
        onChange={handleChange}
        required
      />
      <TextField
        name="userNotes"
        label={t('inhabitant.userNotes')}
        fullWidth
        margin="normal"
        value={inhabitant.userNotes}
        onChange={handleChange}
        required
      />
      <TextField
        name="behavior"
        label={t('inhabitant.behavior')}
        fullWidth
        margin="normal"
        value={inhabitant.behavior}
        onChange={handleChange}
        required
      />
      <TextField
        name="condition"
        label={t('inhabitant.condition')}
        fullWidth
        margin="normal"
        value={inhabitant.condition}
        onChange={handleChange}
        required
      />
      <Button type="submit" variant="contained" color="primary">
        {t('inhabitant.add')}
      </Button>
    </form>
  );
};

export default InhabitantAddForm;

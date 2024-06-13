import React, { useState, useEffect } from 'react';
import { TextField, Button, Typography, MenuItem, Divider } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';
import { getAllSensorData } from '../../../api/sensorData/sensorData';
import { addResearchReport } from '../../../api/researchReports/researchReports';
import { getAquariumsForUser } from '../../../api/aquariums/aquariums';
import NavigationBar from '../../user_components/NavigationBar/NavigationBar';
import { getInhabitantsForAquarium } from '../../../api/inhabitants/inhabitants';

const ResearchReportAddForm = () => {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const [aquariums, setAquariums] = useState([]);
  const [selectedAquarium, setSelectedAquarium] = useState('');
  const [sensorData, setSensorData] = useState([]);
  const [inhabitants, setInhabitants] = useState([]);
  const userId = localStorage.getItem("userId");

  useEffect(() => {
    const fetchAquariums = async () => {
      try {
        const aquariumData = await getAquariumsForUser(userId);
        setAquariums(aquariumData);
      } catch (error) {
        console.error('Error fetching aquariums:', error);
      }
    };

    const fetchSensorData = async () => {
      try {
        const sensorData = await getAllSensorData();
        setSensorData(sensorData);
      } catch (error) {
        console.error('Error fetching sensor data:', error);
      }
    };

    fetchAquariums();
    fetchSensorData();
  }, []);

  useEffect(() => {
    const fetchInhabitants = async () => {
      if (selectedAquarium) {
        try {
          const inhabitantsData = await getInhabitantsForAquarium(selectedAquarium);
          setInhabitants(inhabitantsData);
        } catch (error) {
          console.error('Error fetching inhabitants:', error);
        }
      }
    };

    fetchInhabitants();
  }, [selectedAquarium]);

  const handleChange = (event) => {
    setSelectedAquarium(event.target.value);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    const selectedAquariumDetails = aquariums.find(aq => aq.aquariumId === selectedAquarium);
    const { aquariumType} = selectedAquariumDetails;

    const filteredSensorData = sensorData.filter(data => 
      data.sensorIdentificator === aquariumType || 
      inhabitants.some(inhabitant => inhabitant.name === data.sensorIdentificator)
    );

    const researchReport = {
      aquariumId: selectedAquarium,
      creationDate: new Date().toISOString(),
      sensorData: filteredSensorData
    };
    try {
      await addResearchReport(researchReport);
      navigate('/user/research-report');
    } catch (error) {
      console.error('Error adding research report:', error);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <NavigationBar title={t('researchReport.add')} />
      <Divider />
      <TextField
        select
        label={t('researchReport.selectAquarium')}
        fullWidth
        margin="normal"
        value={selectedAquarium}
        onChange={handleChange}
        required
      >
        {aquariums.map(aquarium => (
          <MenuItem key={aquarium.aquariumId} value={aquarium.aquariumId}>
            {aquarium.aquariumType}
          </MenuItem>
        ))}
      </TextField>
      <Button type="submit" variant="contained" color="primary">
        {t('researchReport.submit')}
      </Button>
    </form>
  );
};

export default ResearchReportAddForm;

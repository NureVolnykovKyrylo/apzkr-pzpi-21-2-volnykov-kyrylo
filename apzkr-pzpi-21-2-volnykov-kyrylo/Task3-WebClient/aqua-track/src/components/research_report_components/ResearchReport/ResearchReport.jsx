import React, { useState, useEffect } from 'react';
import { TextField, List, ListItem, ListItemText, Divider, Button, ListItemSecondaryAction } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { deleteResearchReport, getAllResearchReports } from '../../../api/researchReports/researchReports';
import { getAquariumById } from '../../../api/aquariums/aquariums';
import NavigationBar from '../../user_components/NavigationBar/NavigationBar';
import { Link } from 'react-router-dom';

const ResearchReport = () => {
  const { t } = useTranslation();
  const [researchReports, setResearchReports] = useState([]);
  const [aquariums, setAquariums] = useState({});
  const [filter, setFilter] = useState('');

  useEffect(() => {
    const fetchReports = async () => {
      try {
        const reports = await getAllResearchReports();
        setResearchReports(reports);
        
        const aquariumPromises = reports.map(report => getAquariumById(report.aquariumId));
        const aquariumResults = await Promise.all(aquariumPromises);

        const aquariumMap = {};
        aquariumResults.forEach(aquarium => {
          aquariumMap[aquarium.aquariumId] = aquarium.aquariumType;
        });
        setAquariums(aquariumMap);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };
    fetchReports();
  }, []);

  const handleDelete = async (id) => {
    try {
      await deleteResearchReport(id);
      setResearchReports(researchReports.filter(researchReports => researchReports.researchReportId !== id));
    } catch (error) {
      console.error(error.message);
    }
  };

  const filteredReports = researchReports.filter(report => {
    const aquariumType = aquariums[report.aquariumId];
    return filter ? aquariumType && aquariumType.toLowerCase().includes(filter.toLowerCase()) : true;
  });

  return (
    <div>
      <NavigationBar title={t('researchReport.researchReports')} />
      
      <Divider />
      <Button
        sx={{marginInline: "4px"}}
        variant="contained"
        color="primary"
        component={Link}
        to={`/user/research-report/add`}
      >
        {t('researchReport.add')}
      </Button>

      <TextField
        label={t('researchReport.filterByAquariumType')}
        fullWidth
        margin="normal"
        value={filter}
        onChange={(e) => setFilter(e.target.value)}
      />
      <List sx={{maxWidth: "600px", marginInline: "auto", padding: "20px"}}>
        {filteredReports.map(report => (
          <ListItem key={report.researchReportId}>
            <ListItemText
              primary={`Research Report ID: ${report.researchReportId}`}
              secondary={`Aquarium Type: ${aquariums[report.aquariumId] || t('loading')}`}
            />
            <ListItemSecondaryAction>
              <Button
                variant="contained"
                color="secondary"
                onClick={() => handleDelete(report.researchReportId)}
                sx={{marginInline: "4px"}}
              >
                {t('researchReport.delete')}
              </Button>
            </ListItemSecondaryAction>
          </ListItem>
        ))}
      </List>
    </div>
  );
};

export default ResearchReport;

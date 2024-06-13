import React, { useState, useEffect } from 'react';
import { TextField, List, ListItem, ListItemText, Divider, Button, ListItemSecondaryAction } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { deleteAnalysisReport, getAllAnalysisReports } from '../../../api/analysisReports/analysisReports';
import { getAllResearchReports, getResearchReportById } from '../../../api/researchReports/researchReports';
import { getAquariumById } from '../../../api/aquariums/aquariums';
import NavigationBar from '../../user_components/NavigationBar/NavigationBar';
import { Link, useNavigate } from 'react-router-dom';

const AnalysisReport = () => {
  const { t } = useTranslation();
  const [analysisReports, setAnalysisReports] = useState([]);
  const [researchReports, setResearchReports] = useState([]);
  const [aquariums, setAquariums] = useState({});
  const [filter, setFilter] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    const fetchReports = async () => {
      try {
        const analysisReports = await getAllAnalysisReports();
        setAnalysisReports(analysisReports);
        
        const researchReportPromises = analysisReports.map(report => getResearchReportById(report.researchReportId));
        const researchReports = await Promise.all(researchReportPromises);
        setResearchReports(researchReports);

        const aquariumPromises = researchReports.map(report => getAquariumById(report.aquariumId));
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
      await deleteAnalysisReport(id);
      setAnalysisReports(analysisReports.filter(report => report.analysisReportId !== id));
    } catch (error) {
      console.error(error.message);
    }
  };

  const handleView = (id) => {
    navigate(`/user/analysis-report/view/${id}`);
  };

  const filteredReports = analysisReports.filter(report => {
    const researchReport = researchReports.find(r => r.researchReportId === report.researchReportId);
    const aquariumType = researchReport && aquariums[researchReport.aquariumId];
    return filter ? aquariumType && aquariumType.toLowerCase().includes(filter.toLowerCase()) : true;
  });

  return (
    <div>
      <NavigationBar title={t('analysisReport.analysisReports')} />
      
      <Divider />
      <Button
        sx={{ marginInline: "4px" }}
        variant="contained"
        color="primary"
        component={Link}
        to={`/user/analysis-report/add`}
      >
        {t('analysisReport.add')}
      </Button>

      <TextField
        label={t('analysisReport.filterByAquariumType')}
        fullWidth
        margin="normal"
        value={filter}
        onChange={(e) => setFilter(e.target.value)}
      />
      <List sx={{ maxWidth: "600px", marginInline: "auto", padding: "20px" }}>
        {filteredReports.map(report => {
          const researchReport = researchReports.find(r => r.researchReportId === report.researchReportId);
          const aquariumType = researchReport && aquariums[researchReport.aquariumId];
          return (
            <ListItem key={report.analysisReportId}>
              <ListItemText
                primary={`Analysis Report ID: ${report.analysisReportId}`}
                secondary={`Aquarium Type: ${aquariumType || t('loading')}`}
              />
              <ListItemSecondaryAction>
                <Button
                  variant="contained"
                  color="secondary"
                  onClick={() => handleDelete(report.analysisReportId)}
                  sx={{ marginInline: "4px" }}
                >
                  {t('analysisReport.delete')}
                </Button>
                <Button
                  variant="contained"
                  color="primary"
                  onClick={() => handleView(report.analysisReportId)}
                  sx={{ marginInline: "4px" }}
                >
                  {t('analysisReport.view')}
                </Button>
              </ListItemSecondaryAction>
            </ListItem>
          );
        })}
      </List>
    </div>
  );
};

export default AnalysisReport;

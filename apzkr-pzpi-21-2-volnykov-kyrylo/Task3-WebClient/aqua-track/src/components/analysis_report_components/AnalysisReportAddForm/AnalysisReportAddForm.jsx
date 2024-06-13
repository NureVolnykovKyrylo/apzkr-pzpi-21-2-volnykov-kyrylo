import React, { useState, useEffect } from 'react';
import { TextField, Button, Divider, MenuItem } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';
import { getAllSensorData } from '../../../api/sensorData/sensorData';
import { addAnalysisReport } from '../../../api/analysisReports/analysisReports';
import { getAllResearchReports, getResearchReportById } from '../../../api/researchReports/researchReports';
import NavigationBar from '../../user_components/NavigationBar/NavigationBar';

const AnalysisReportAddForm = () => {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const [researchReports, setResearchReports] = useState([]);
  const [selectedResearchReport, setSelectedResearchReport] = useState('');
  const userId = localStorage.getItem("userId");

  useEffect(() => {
    const fetchResearchReports = async () => {
      try {
        const reports = await getAllResearchReports();
        setResearchReports(reports);
      } catch (error) {
        console.error('Error fetching research reports:', error);
      }
    };

    fetchResearchReports();
  }, []);

  const handleChange = (event) => {
    setSelectedResearchReport(event.target.value);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    try {
      await addAnalysisReport(selectedResearchReport);
      console.log(selectedResearchReport);
      navigate('/user/analysis-report');
    } catch (error) {
      console.error('Error adding analysis report:', error);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <NavigationBar title={t('analysisReport.add')} />
      <Divider />
      <TextField
        select
        label={t('analysisReport.selectResearchReport')}
        fullWidth
        margin="normal"
        value={selectedResearchReport}
        onChange={handleChange}
        required
      >
        {researchReports.map(report => (
          <MenuItem key={report.researchReportId} value={report.researchReportId}>
            {`Research Report ID: ${report.researchReportId}`}
          </MenuItem>
        ))}
      </TextField>
      <Button type="submit" variant="contained" color="primary">
        {t('analysisReport.submit')}
      </Button>
    </form>
  );
};

export default AnalysisReportAddForm;

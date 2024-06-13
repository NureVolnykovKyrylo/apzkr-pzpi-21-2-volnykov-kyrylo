import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { Typography, Divider, Paper, Box } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { getAnalysisReportById } from '../../../api/analysisReports/analysisReports';
import NavigationBar from '../../user_components/NavigationBar/NavigationBar';

const AnalysisReportView = () => {
  const { t } = useTranslation();
  const { analysisReportId } = useParams();
  const [analysisReport, setAnalysisReport] = useState(null);

  useEffect(() => {
    const fetchReport = async () => {
      try {
        const report = await getAnalysisReportById(analysisReportId);
        setAnalysisReport(report);
      } catch (error) {
        console.error('Error fetching analysis report:', error);
      }
    };
    fetchReport();
  }, [analysisReportId]);

  if (!analysisReport) {
    return <Typography>{t('loading')}</Typography>;
  }

  return (
    <div>
      <NavigationBar title={t('analysisReport.view')} />
      <Divider />
      <Paper sx={{ maxWidth: "600px", margin: "auto", padding: "20px" }}>
        <Typography variant="h5" gutterBottom>
          {analysisReport.title}
        </Typography>
        <Typography variant="subtitle1" gutterBottom>
          {t('analysisReport.creationDate')}: {new Date(analysisReport.creationDate).toLocaleString()}
        </Typography>
        <Divider sx={{ marginY: "10px" }} />
        <Box sx={{ whiteSpace: 'pre-line' }}>
          <Typography variant="h6" gutterBottom>
            {t('analysisReport.identifiedTrends')}
          </Typography>
          <Typography variant="body1" gutterBottom>
            {analysisReport.identifiedTrends}
          </Typography>
          <Divider sx={{ marginY: "10px" }} />
          <Typography variant="h6" gutterBottom>
            {t('analysisReport.recommendations')}
          </Typography>
          <Typography variant="body1">
            {analysisReport.recommendations}
          </Typography>
        </Box>
      </Paper>
    </div>
  );
};

export default AnalysisReportView;

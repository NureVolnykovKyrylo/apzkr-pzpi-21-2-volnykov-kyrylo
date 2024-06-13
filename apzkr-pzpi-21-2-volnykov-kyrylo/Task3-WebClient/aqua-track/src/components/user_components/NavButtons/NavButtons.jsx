import React from 'react';
import { Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

const NavButtons = () => {
  const navigate = useNavigate();
  const { t } = useTranslation();

  const navigateTo = (path) => {
    navigate(path);
  };

  return (
    <>
        <Button color="inherit" onClick={() => navigateTo('/user/research-report')}>
            {t('navigationBar.researchReport')}
        </Button>
        <Button color="inherit" onClick={() => navigateTo('/user/analysis-report')}>
            {t('navigationBar.analysisReport')}
        </Button>
    </>
    
  );
};

export default NavButtons;
import React from 'react';
import { useTranslation } from 'react-i18next';
import { Select, MenuItem } from '@mui/material';
import languages from "../../utils/languages";

export default function LanguageSwitcher() {
  const { i18n } = useTranslation();
  
  const handleChange = (event) => {
    i18n.changeLanguage(event.target.value);
  };
  return (
    <Select
      className="border border-slate-800 bg-white mb-5 p-1 rounded w-fit"
      value={i18n.resolvedLanguage}
      onChange={handleChange}
    >
      {languages.map((lang, i) => (
        <MenuItem key={i} value={lang.code}>{lang.code.toUpperCase()}</MenuItem>
      ))}
    </Select>
  );
}

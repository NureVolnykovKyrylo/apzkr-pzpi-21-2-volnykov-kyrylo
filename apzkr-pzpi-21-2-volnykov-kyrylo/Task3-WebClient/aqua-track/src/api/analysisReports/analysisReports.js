import axios from 'axios';

const API_URL = `${import.meta.env.VITE_API_URL}/api/AnalysisReport`;


export const getAllAnalysisReports = async () => {
  try {
    const response = await axios.get(API_URL);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to fetch analysis reports');
  }
};

export const getAnalysisReportById = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to fetch analysis report');
  }
};

export const addAnalysisReport = async (researchReportId) => {
  try {
    const response = await axios.post(`${API_URL}/${researchReportId}`);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to add analysis report');
  }
};

export const updateAnalysisReport = async (id, analysisReport) => {
  try {
    const response = await axios.put(`${API_URL}/${id}`, analysisReport);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to update analysis report');
  }
};

export const deleteAnalysisReport = async (id) => {
  try {
    await axios.delete(`${API_URL}/${id}`);
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to delete analysis report');
  }
};


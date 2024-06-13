import axios from 'axios';

export const API_URL = `${import.meta.env.VITE_API_URL}/api/ResearchReport`;

export const getAllResearchReports = async () => {
  try {
    const response = await axios.get(API_URL);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to fetch research reports');
  }
};

export const getResearchReportById = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to fetch research report');
  }
};

export const addResearchReport = async (report) => {
  try {
    const response = await axios.post(API_URL, report);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to add research report');
  }
};

export const updateResearchReport = async (id, report) => {
  try {
    const response = await axios.put(`${API_URL}/${id}`, report);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to update research report');
  }
};

export const deleteResearchReport = async (id) => {
  try {
    await axios.delete(`${API_URL}/${id}`);
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to delete research report');
  }
};


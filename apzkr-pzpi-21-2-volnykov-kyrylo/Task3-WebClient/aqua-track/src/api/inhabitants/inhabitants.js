import axios from 'axios';

const API_URL = `${import.meta.env.VITE_API_URL}/api/Inhabitant`;

export const getInhabitantsForAquarium = async (aquariumId) => {
  try {
    const response = await axios.get(`${API_URL}/aquarium/${aquariumId}`);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to fetch inhabitants');
  }
};

export const getInhabitantById = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to fetch inhabitant');
  }
};

export const addInhabitantForCurrentUser = async (inhabitant) => {
  try {
    const response = await axios.post(API_URL, inhabitant);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to add inhabitant');
  }
};

export const updateInhabitantForCurrentUser = async (id, inhabitant) => {
  try {
    const response = await axios.put(`${API_URL}/${id}`, inhabitant);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to update inhabitant');
  }
};

export const deleteInhabitantForCurrentUser = async (id) => {
  try {
    await axios.delete(`${API_URL}/${id}`);
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to delete inhabitant');
  }
};



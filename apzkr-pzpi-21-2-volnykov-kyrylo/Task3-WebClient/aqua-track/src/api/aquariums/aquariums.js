import axios from 'axios';

const API_URL = `${import.meta.env.VITE_API_URL}/api/Aquarium`;

export const getAquariumById = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to fetch aquarium');
  }
};

export const getAquariumsForCurrentUser = async () => {
  try {
    const response = await axios.get(`${API_URL}/current-user`);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to fetch aquariums for current user');
  }
};

export const getAquariumsForUser = async (userId) => {
  try {
    const response = await axios.get(`${API_URL}/user/${userId}`);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to fetch aquariums for user');
  }
};

export const addAquariumForUser = async (aquarium, userId) => {
  try {
    const response = await axios.post(`${API_URL}/user/${userId}`, aquarium);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to add aquarium');
  }
};

export const updateAquariumForUser = async (id, aquarium, userId) => {
  try {
    const response = await axios.put(`${API_URL}/${id}/user/${userId}`, aquarium);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to update aquarium');
  }
};

export const deleteAquariumForUser = async (id, userId) => {
  try {
    const response = await axios.delete(`${API_URL}/${id}/user/${userId}`);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to delete aquarium');
  }
};


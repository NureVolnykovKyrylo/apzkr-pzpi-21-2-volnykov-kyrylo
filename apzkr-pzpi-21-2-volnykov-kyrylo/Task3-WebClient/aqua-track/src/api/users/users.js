import axios from 'axios';

const API_URL = `${import.meta.env.VITE_API_URL}/api/User`;

export const login = async (email, password) => {
  try {
    const response = await axios.post(`${API_URL}/login`, { email, password });
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Login failed');
  }
};

export const register = async (userData) => {
  try {
    const response = await axios.post(`${API_URL}/register`, userData);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Registration failed');
  }
};

export const getUserById = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Get user by id failed');
  }
};

export const getCurrentUserInfo = async () => {
  try {
    const response = await axios.get(`${API_URL}/userinfo`);
    console.log(response);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to fetch user info');
  }
};

export const updateUserInfo = async (userData) => {
  try {
    const response = await axios.put(`${API_URL}/update`, userData);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Update failed');
  }
};

export const logout = async () => {
  try {
    const response = await axios.post(`${API_URL}/logout`);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Logout failed');
  }
};

export const getAllUsers = async () => {
  try {
    const response = await axios.get(`${API_URL}/all`);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to fetch users');
  }
};

export const deleteUser = async (userId) => {
  try {
    const response = await axios.delete(`${API_URL}/${userId}`);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || `Failed to delete user with ID ${userId}`);
  }
};

export const addUser = async (userData) => {
  try {
    const response = await axios.post(`${API_URL}/add`, userData);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to add user');
  }
};

export const updateUser = async (userId, userData) => {
  try {
    const response = await axios.put(`${API_URL}/update/${userId}`, userData);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || `Failed to update user with ID ${userId}`);
  }
};

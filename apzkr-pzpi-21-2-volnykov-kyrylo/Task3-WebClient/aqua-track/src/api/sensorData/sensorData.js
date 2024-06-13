import axios from 'axios';

const API_URL = `${import.meta.env.VITE_API_URL}/api/SensorData`;

export const getAllSensorData = async () => {
  try {
    const response = await axios.get(API_URL);
    return response.data;
  } catch (error) {
    console.error('Error fetching sensor data:', error);
    throw error;
  }
};

export const getSensorDataById = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data;
  } catch (error) {
    console.error(`Error fetching sensor data by id (${id}):`, error);
    throw error;
  }
};

export const addSensorData = async (sensorDataViewModel) => {
  try {
    const response = await axios.post(API_URL, sensorDataViewModel);
    return response.data;
  } catch (error) {
    console.error('Error adding sensor data:', error);
    throw error;
  }
};

export const updateSensorData = async (id, sensorDataUpdateViewModel) => {
  try {
    const response = await axios.put(`${API_URL}/${id}`, sensorDataUpdateViewModel);
    return response.data;
  } catch (error) {
    console.error(`Error updating sensor data (${id}):`, error);
    throw error;
  }
};

export const deleteSensorData = async (id) => {
  try {
    await axios.delete(`${API_URL}/${id}`);
  } catch (error) {
    console.error(`Error deleting sensor data (${id}):`, error);
    throw error;
  }
};

export const findSensorBySensorIdentificatorAndType = async (sensorIdentificator, sensorType) => {
  try {
    const response = await axios.get(`${API_URL}/find`, {
      params: { sensorIdentificator, sensorType },
    });
    return response.data;
  } catch (error) {
    console.error(`Error finding sensor data by sensorIdentificator (${sensorIdentificator}) and type (${sensorType}):`, error);
    throw error;
  }
};

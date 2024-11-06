import api from "../config/apiConfig";
import { Customer, CustomerRequest } from "../interfaces/CustomerInterface";

export const createCustomer = async (customerData: CustomerRequest) => {
  const response = await api.post('/api/customers', customerData);
  return response.data;
};

export const getCustomers = async () => {
  const response = await api.get<Customer[]>('/api/customers');
  return response.data;
};

export const getCustomerById = async (customerId: number) => {
  const response = await api.get<Customer>(`/api/customers/${customerId}`);
  return response.data;
};

export const updateCustomer = async (customerId: number, customerData: CustomerRequest) => {
  const response = await api.put(`/api/customers/${customerId}`, customerData);
  return response.data;
};

export const deleteCustomer = async (customerId: number) => {
  const response = await api.delete(`/api/customers/${customerId}`);
  return response.data;
};

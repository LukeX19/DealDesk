import api from "../config/apiConfig";
import { Product, ProductRequest } from "../interfaces/ProductInterface";

export const createProduct = async (productData: ProductRequest) => {
  const response = await api.post('/api/products', productData);
  return response.data;
};

export const getProducts = async () => {
  const response = await api.get<Product[]>('/api/products');
  return response.data;
};

export const getProductById = async (productId: number) => {
  const response = await api.get<Product>(`/api/products/${productId}`);
  return response.data;
};

export const updateProduct = async (productId: number, productData: ProductRequest) => {
  const response = await api.put(`/api/products/${productId}`, productData);
  return response.data;
};

export const deleteProduct = async (productId: number) => {
  const response = await api.delete(`/api/products/${productId}`);
  return response.data;
};

export const getDiscountedPrice = async (productId: number, quantity: number, customerId: number) => {
  const response = await api.get(`/api/products/discount`, {
    params: { productId, quantity, customerId },
  });
  return response.data;
};

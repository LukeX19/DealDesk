import React, { useEffect, useState } from 'react';
import { Container, Typography, Button, Box } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import ProductsTable from '../components/ProductsTable';
import CustomersTable from '../components/CustomersTable';
import ProductDialog from '../components/ProductDialog';
import CustomerDialog from '../components/CustomerDialog';
import { Product } from '../interfaces/ProductInterface';
import { Customer } from '../interfaces/CustomerInterface';
import * as ProductService from '../services/ProductService';
import * as CustomerService from '../services/CustomerService';
import DiscountDialog from '../components/DiscountDialog';

const Home: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [customers, setCustomers] = useState<Customer[]>([]);
  const [productDialogOpen, setProductDialogOpen] = useState(false);
  const [customerDialogOpen, setCustomerDialogOpen] = useState(false);
  const [discountDialogOpen, setDiscountDialogOpen] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);
  const [selectedCustomer, setSelectedCustomer] = useState<Customer | null>(null);

  useEffect(() => {
    loadProducts();
    loadCustomers();
  }, []);

  const loadProducts = async () => {
    try {
      const response = await ProductService.getProducts();
      setProducts(response);
    } catch (error) {
      console.error("Failed to load products:", error);
    }
  };

  const loadCustomers = async () => {
    try {
      const response = await CustomerService.getCustomers();
      setCustomers(response);
    } catch (error) {
      console.error("Failed to load customers:", error);
    }
  };

  const deleteProduct = async (productId: number) => {
    try {
      await ProductService.deleteProduct(productId);
      loadProducts();
    } catch (error) {
      console.error("Failed to delete product:", error);
    }
  };

  const deleteCustomer = async (customerId: number) => {
    try {
      await CustomerService.deleteCustomer(customerId);
      loadCustomers();
    } catch (error) {
      console.error("Failed to delete customer:", error);
    }
  };

  return (
    <Container>
      <Box
        display="flex"
        flexDirection="column"
        alignItems="center"
        justifyContent="center"
        textAlign="center"
        mt={4}
        mb={10}
      >
        <Typography variant="h3">DealDesk</Typography>
        <Typography variant="h6" sx={{ fontStyle: 'italic' }}>
          A simple discount calculator.
        </Typography>
      </Box>

      <Box mb={6}>
        <Typography variant="h4" gutterBottom>Products</Typography>
        <Button
          variant="contained"
          startIcon={<AddIcon />}
          onClick={() => { setSelectedProduct(null); setProductDialogOpen(true); }}
          sx={{ mb: 2 }}
        >
          Add
        </Button>
        {products.length > 0 ? (
          <ProductsTable
            products={products}
            onEdit={(product) => { setSelectedProduct(product); setProductDialogOpen(true); }}
            onDelete={deleteProduct}
          />
        ) : (
          <Typography>No products available yet. Please add a product.</Typography>
        )}
      </Box>

      <Box mt={8} mb={6}>
        <Typography variant="h4" gutterBottom>Customers</Typography>
        <Button
          variant="contained"
          startIcon={<AddIcon />}
          onClick={() => { setSelectedCustomer(null); setCustomerDialogOpen(true); }}
          sx={{ mb: 2 }}
        >
          Add
        </Button>
        {customers.length > 0 ? (
          <CustomersTable
            customers={customers}
            onEdit={(customer) => { setSelectedCustomer(customer); setCustomerDialogOpen(true); }}
            onDelete={deleteCustomer}
          />
        ) : (
          <Typography>No customers available yet. Please add a customer.</Typography>
        )}
      </Box>

      <Box mt={4}>
        <Button
          variant="contained"
          color="primary"
          onClick={() => setDiscountDialogOpen(true)}
          sx={{ mb: 2 }}
        >
          Calculate Discount
        </Button>
      </Box>

      <ProductDialog
        open={productDialogOpen}
        onClose={() => setProductDialogOpen(false)}
        onSaveSuccess={loadProducts}
        initialData={selectedProduct}
      />
      <CustomerDialog
        open={customerDialogOpen}
        onClose={() => setCustomerDialogOpen(false)}
        onSaveSuccess={loadCustomers}
        initialData={selectedCustomer}
      />
      <DiscountDialog
        open={discountDialogOpen}
        onClose={() => setDiscountDialogOpen(false)}
        products={products}
        customers={customers}
      />
    </Container>
  );
};

export default Home;

import React, { useEffect, useState } from 'react';
import { Dialog, DialogActions, DialogContent, DialogTitle, TextField, Button, MenuItem, Box, Typography } from '@mui/material';
import { Product } from '../interfaces/ProductInterface';
import { Customer } from '../interfaces/CustomerInterface';
import * as ProductService from '../services/ProductService';

interface DiscountDialogProps {
  open: boolean;
  onClose: () => void;
  products: Product[];
  customers: Customer[];
}

const DiscountDialog: React.FC<DiscountDialogProps> = ({ open, onClose, products, customers }) => {
  const [selectedProductId, setSelectedProductId] = useState<number | null>(null);
  const [selectedCustomerId, setSelectedCustomerId] = useState<number | null>(null);
  const [discountedPrice, setDiscountedPrice] = useState<number | null>(null);

  useEffect(() => {
    if (open) {
      setSelectedProductId(null);
      setSelectedCustomerId(null);
      setDiscountedPrice(null);
    }
  }, [open]);

  const handleCalculateDiscount = async () => {
    if (selectedProductId && selectedCustomerId) {
      try {
        const response = await ProductService.getDiscountedPrice(selectedProductId, selectedCustomerId);
        setDiscountedPrice(response.discountedPrice);
      } catch (error) {
        console.error("Failed to load discounted price:", error);
      }
    }
  };

  return (
    <Dialog open={open} onClose={onClose} fullWidth>
      <DialogTitle>Calculate Discounted Price</DialogTitle>
      <DialogContent>
        <TextField
          select
          label="Select Product"
          value={selectedProductId || ''}
          onChange={(e) => setSelectedProductId(Number(e.target.value))}
          fullWidth
          margin="dense"
        >
          {products.map((product) => (
            <MenuItem key={product.id} value={product.id}>
              {product.name}
            </MenuItem>
          ))}
        </TextField>

        <TextField
          select
          label="Select Customer"
          value={selectedCustomerId || ''}
          onChange={(e) => setSelectedCustomerId(Number(e.target.value))}
          fullWidth
          margin="dense"
        >
          {customers.map((customer) => (
            <MenuItem key={customer.id} value={customer.id}>
              {customer.name}
            </MenuItem>
          ))}
        </TextField>

        {discountedPrice !== null && (
          <Box mt={2}>
            <Typography variant="h6">
              Discounted Price: ${discountedPrice}
            </Typography>
          </Box>
        )}
      </DialogContent>
      <DialogActions>
        <Button
          onClick={onClose}
          sx={{ color: 'black' }}
        >
          Cancel
        </Button>
        <Button
          onClick={handleCalculateDiscount}
          variant="contained"
          color="primary"
          disabled={!selectedProductId || !selectedCustomerId}
        >
          Calculate
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default DiscountDialog;
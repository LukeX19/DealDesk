import React, { useEffect, useState } from 'react';
import { Dialog, DialogActions, DialogContent, DialogTitle, TextField, Button } from '@mui/material';
import { Product, ProductRequest } from '../interfaces/ProductInterface';
import * as ProductService from '../services/ProductService';

interface ProductDialogProps {
  open: boolean;
  onClose: () => void;
  onSaveSuccess: () => void;
  initialData?: Product | null;
}

const ProductDialog: React.FC<ProductDialogProps> = ({ open, onClose, onSaveSuccess, initialData }) => {
  const [product, setProduct] = useState<ProductRequest>({ name: '', standardPrice: 0 });

  useEffect(() => {
    if (initialData) {
      setProduct({ name: initialData.name, standardPrice: initialData.standardPrice });
    } else {
      setProduct({ name: '', standardPrice: 0 });
    }
  }, [initialData, open]);

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setProduct({
      ...product,
      [name]: name === 'standardPrice' ? parseFloat(value) : value
    });
  };

  const handleSave = async () => {
    if (initialData) {
      try {
        await ProductService.updateProduct(initialData.id, product);
      } catch (error) {
        console.error("Failed to update product:", error);
      }
    } else {
      try {
        await ProductService.createProduct(product);
      } catch (error) {
        console.error("Failed to add product:", error);
      }
    }
    onSaveSuccess();
    onClose();
  };

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>{initialData ? 'Edit Product' : 'Add Product'}</DialogTitle>
      <DialogContent>
        <TextField
          label="Name"
          name="name"
          value={product.name}
          onChange={handleChange}
          fullWidth
          margin="dense"
        />
        <TextField
          label="Standard Price"
          name="standardPrice"
          value={product.standardPrice}
          onChange={handleChange}
          fullWidth
          margin="dense"
          type="number"
        />
      </DialogContent>
      <DialogActions>
        <Button
          onClick={onClose}
          sx={{ color: 'black' }}
        >
          Cancel
        </Button>
        <Button
          onClick={handleSave}
          variant="contained"
          color="primary"
          disabled={!product.name.trim() || product.standardPrice <= 0}
        >
          Save
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default ProductDialog;

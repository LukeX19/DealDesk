import React, { useEffect, useState } from 'react';
import { Dialog, DialogActions, DialogContent, DialogTitle, TextField, Button } from '@mui/material';
import { Customer, CustomerRequest } from '../interfaces/CustomerInterface';
import * as CustomerService from '../services/CustomerService';

interface CustomerDialogProps {
  open: boolean;
  onClose: () => void;
  onSaveSuccess: () => void;
  initialData?: Customer | null;
}

const CustomerDialog: React.FC<CustomerDialogProps> = ({ open, onClose, onSaveSuccess, initialData }) => {
  const [customer, setCustomer] = useState<CustomerRequest>({ name: '', discountStrategies: [] });

  useEffect(() => {
    if (initialData) {
      setCustomer({ name: initialData.name, discountStrategies: initialData.discountStrategies });
    } else {
      setCustomer({ name: '', discountStrategies: [] });
    }
  }, [initialData, open]);

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setCustomer({
      ...customer,
      [name]: name === 'discountStrategies' ? value.split(',') : value
    });
  };

  const handleSave = async () => {
    const trimmedCustomer = {
      ...customer,
      discountStrategies: customer.discountStrategies.map(strategy => strategy.trim()),
    };
    
    if (initialData) {
      try {
        await CustomerService.updateCustomer(initialData.id, trimmedCustomer);
      } catch (error) {
        console.error("Failed to update customer:", error);
      }
    } else {
      try {
        await CustomerService.createCustomer(trimmedCustomer);
      } catch (error) {
        console.error("Failed to add customer:", error);
      }
    }
    onSaveSuccess();
    onClose();
  };

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>{initialData ? 'Edit Customer' : 'Add Customer'}</DialogTitle>
      <DialogContent>
        <TextField
          label="Name"
          name="name"
          value={customer.name}
          onChange={handleChange}
          fullWidth
          margin="dense"
        />
        <TextField
          label="Discount Strategies"
          name="discountStrategies"
          value={customer.discountStrategies.join(',')}
          onChange={handleChange}
          fullWidth
          margin="dense"
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
          disabled={!customer.name.trim()}
        >
          Save
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default CustomerDialog;

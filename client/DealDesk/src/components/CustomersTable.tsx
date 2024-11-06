import React from 'react';
import { Customer } from '../interfaces/CustomerInterface';
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Button } from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';

interface CustomerTableProps {
  customers: Customer[];
  onEdit: (customer: Customer) => void;
  onDelete: (id: number) => void;
}

const CustomerTable: React.FC<CustomerTableProps> = ({ customers, onEdit, onDelete }) => {
  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell align="center" sx={{ width: '25%' }}>Id</TableCell>
            <TableCell align="center" sx={{ width: '25%' }}>Name</TableCell>
            <TableCell align="center" sx={{ width: '25%' }}>Discount Strategies</TableCell>
            <TableCell align="center" sx={{ width: '25%' }}>Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {customers.map((customer) => (
            <TableRow key={customer.id}>
              <TableCell align="center">{customer.id}</TableCell>
              <TableCell align="center">{customer.name}</TableCell>
              <TableCell align="center">{customer.discountStrategies.join(', ')}</TableCell>
              <TableCell align="center">
                <Button
                  onClick={() => onEdit(customer)}
                  variant="contained"
                  color="secondary"
                  startIcon={<EditIcon />}
                  sx={{ mr: 1 }}
                >
                  Edit
                </Button>
                <Button
                  onClick={() => onDelete(customer.id)}
                  variant="contained"
                  color="error"
                  startIcon={<DeleteIcon />}
                >
                  Delete
                </Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default CustomerTable;

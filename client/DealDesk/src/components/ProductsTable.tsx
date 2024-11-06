import React from 'react';
import { Product } from '../interfaces/ProductInterface';
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Button } from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';

interface ProductTableProps {
  products: Product[];
  onEdit: (product: Product) => void;
  onDelete: (id: number) => void;
}

const ProductsTable: React.FC<ProductTableProps> = ({ products, onEdit, onDelete }) => {
  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell align="center" sx={{ width: '25%' }}>Id</TableCell>
            <TableCell align="center" sx={{ width: '25%' }}>Name</TableCell>
            <TableCell align="center" sx={{ width: '25%' }}>Standard Price</TableCell>
            <TableCell align="center" sx={{ width: '25%' }}>Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {products.map((product) => (
            <TableRow key={product.id}>
              <TableCell align="center">{product.id}</TableCell>
              <TableCell align="center">{product.name}</TableCell>
              <TableCell align="center">€ {product.standardPrice}</TableCell>
              <TableCell align="center">
                <Button
                  onClick={() => onEdit(product)}
                  variant="contained"
                  color="secondary"
                  startIcon={<EditIcon />}
                  sx={{ mr: 1 }}
                >
                  Edit
                </Button>
                <Button
                  onClick={() => onDelete(product.id)}
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

export default ProductsTable;

import { createTheme, ThemeProvider } from '@mui/material';
import Home from './pages/Home'

const theme = createTheme({
  palette: {
    primary: {
      main: '#90D26D'
    },
    secondary: {
      main: '#FFD44D',
    },
  },
  typography: {
    fontFamily: 'Poppins, sans-serif',
  }
});

function App() {
  return (
    <ThemeProvider theme={theme}>
      <Home/>
    </ThemeProvider>
  );
};

export default App;

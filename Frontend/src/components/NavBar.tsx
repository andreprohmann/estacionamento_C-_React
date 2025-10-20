
import { AppBar, Toolbar, Typography, Button, Stack } from '@mui/material';
import { Link as RouterLink, useLocation } from 'react-router-dom';

export default function NavBar() {
  const { pathname } = useLocation();

  const LinkBtn = ({ to, children }: { to: string; children: React.ReactNode }) => (
    <Button
      color={pathname === to ? 'secondary' : 'inherit'}
      component={RouterLink}
      to={to}
      sx={{ textTransform: 'none' }}
    >
      {children}
    </Button>
  );

  return (
    <AppBar position="sticky">
      <Toolbar sx={{ display: 'flex', gap: 2 }}>
        <Typography variant="h6" sx={{ flexGrow: 1 }}>
          Estacionamento
        </Typography>
        <Stack direction="row" spacing={1}>
          <LinkBtn to="/">Dashboard</LinkBtn>
          <LinkBtn to="/veiculos">Veículos</LinkBtn>
          <LinkBtn to="/checkin">Check‑in</LinkBtn>
          <LinkBtn to="/checkout">Check‑out</LinkBtn>
        </Stack>
      </Toolbar>
    </AppBar>
  );
}

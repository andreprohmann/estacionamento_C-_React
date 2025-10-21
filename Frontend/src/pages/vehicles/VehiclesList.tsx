import { useEffect, useState } from 'react';
import { listVehicles } from '../../services/parkingService';
import { Vehicle } from '../../types';
import {
  Alert, CircularProgress, Container, Paper, Table, TableBody, TableCell,
  TableContainer, TableHead, TableRow, Typography
} from '@mui/material';

export default function VehiclesList() {
  const [rows, setRows] = useState<Vehicle[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string>();

  useEffect(() => {
    (async () => {
      try {
        const data = await listVehicles();
        setRows(data);
      } catch (e: any) {
        setError(e?.message ?? 'Erro ao carregar veículos');
      } finally {
        setLoading(false);
      }
    })();
  }, []);

  return (
    <Container sx={{ py: 3 }}>
      <Typography variant="h5" gutterBottom>Veículos no pátio</Typography>
      {loading && <CircularProgress />}
      {error && <Alert severity="error">{error}</Alert>}

      {!loading && !error && (
        <TableContainer component={Paper}>
          <Table size="small">
            <TableHead>
              <TableRow>
                <TableCell>Placa</TableCell>
                <TableCell>Modelo</TableCell>
                <TableCell>Cor</TableCell>
                <TableCell>Motorista</TableCell>
                <TableCell>Entrada</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {rows.map((v) => (
                <TableRow key={v.plate}>
                  <TableCell>{v.plate}</TableCell>
                  <TableCell>{v.model ?? '—'}</TableCell>
                  <TableCell>{v.color ?? '—'}</TableCell>
                  <TableCell>{v.driverName ?? '—'}</TableCell>
                  <TableCell>{v.timeIn ? new Date(v.timeIn).toLocaleString() : '—'}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}
    </Container>
  );
}
``
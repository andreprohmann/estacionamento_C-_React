
import { useEffect, useState } from 'react';
import { listVehicles } from '../../services/parkingService';
import type { VeiculoDto } from '../../types';
import {
  Alert, CircularProgress, Container, Paper, Table, TableBody, TableCell,
  TableContainer, TableHead, TableRow, Typography
} from '@mui/material';

export default function VehiclesList() {
  const [rows, setRows] = useState<VeiculoDto[]>([]);
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
              </TableRow>
            </TableHead>
            <TableBody>
              {rows.map((v) => (
                <TableRow key={v.placa} hover>
                  <TableCell>{v.placa}</TableCell>
                  <TableCell>{v.modelo ?? '—'}</TableCell>
                  <TableCell>{v.cor ?? '—'}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}
    </Container>
  );
}

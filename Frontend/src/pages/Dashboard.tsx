
import { useEffect, useState } from 'react';
import { Alert, Card, CardContent, CircularProgress, Typography } from '@mui/material';
import Grid from '@mui/material/Unstable_Grid2';
import { getOcupacaoAtual } from '../services/parkingService';
import type { OcupacaoAtual } from '../types';

export default function Dashboard() {
  const [status, setStatus] = useState<OcupacaoAtual | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string>();

  useEffect(() => {
    (async () => {
      try {
        const s = await getOcupacaoAtual();
        setStatus(s);
      } catch (e: any) {
        setError(e?.message ?? 'Erro ao carregar ocupação atual');
      } finally {
        setLoading(false);
      }
    })();
  }, []);

  if (loading) return <CircularProgress sx={{ m: 3 }} />;
  if (error) return <Alert severity="error">{error}</Alert>;

  return (
    <Grid container spacing={2} sx={{ p: 2 }}>
      <Grid size={{ xs: 12, md: 4 }}>
        <Card><CardContent>
          <Typography variant="h6">Vagas Totais</Typography>
          <Typography variant="h3">{status?.totalSpots ?? '—'}</Typography>
        </CardContent></Card>
      </Grid>
      <Grid size={{ xs: 12, md: 4 }}>
        <Card><CardContent>
          <Typography variant="h6">Ocupadas</Typography>
          <Typography variant="h3">{status?.occupied ?? '—'}</Typography>
        </CardContent></Card>
      </Grid>
      <Grid size={{ xs: 12, md: 4 }}>
        <Card><CardContent>
          <Typography variant="h6">Disponíveis</Typography>
          <Typography variant="h3">{status?.available ?? '—'}</Typography>
        </CardContent></Card>
      </Grid>
    </Grid>
  );
}

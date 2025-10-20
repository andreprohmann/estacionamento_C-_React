
import { z } from 'zod';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { Alert, Box, Button, Container, Stack, TextField, Typography } from '@mui/material';
import { checkoutVehicle } from '../../services/parkingService';

export const plateRegex = /^(?:[A-Z]{3}-?\d{4}|[A-Z]{3}\d[A-Z]\d{2})$/i;

const schema = z.object({ placa: z.string().regex(plateRegex, 'Placa inválida') });

type FormData = z.infer<typeof schema>;

export default function VehicleCheckout() {
  const { register, handleSubmit, formState: { errors, isSubmitting } } =
    useForm<FormData>({ resolver: zodResolver(schema) });

  const onSubmit = async ({ placa }: FormData) => {
    const res = await checkoutVehicle(placa.toUpperCase().replace('-', ''));
    const price = res?.price ?? 0;
    alert(`Check‑out OK. Valor a pagar: R$ ${price.toFixed(2)}`);
  };

  return (
    <Container maxWidth="sm" sx={{ py: 3 }}>
      <Typography variant="h5" gutterBottom>Check‑out</Typography>
      <Box component="form" onSubmit={handleSubmit(onSubmit)} noValidate>
        <Stack spacing={2}>
          <TextField
            label="Placa"
            {...register('placa')}
            error={!!errors.placa}
            helperText={errors.placa?.message}
            inputProps={{ style: { textTransform: 'uppercase' } }}
          />
          <Button type="submit" variant="contained" disabled={isSubmitting}>
            {isSubmitting ? 'Processando...' : 'Registrar Check‑out'}
          </Button>
          <Alert severity="info">Proxy do Vite → /estacionamento/checkout → http://localhost:5082</Alert>
        </Stack>
      </Box>
    </Container>
  );
}

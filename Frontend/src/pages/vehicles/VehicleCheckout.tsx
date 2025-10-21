import { z } from 'zod';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { Alert, Box, Button, Container, Stack, TextField, Typography } from '@mui/material';
import { checkoutVehicle } from '../../services/parkingService';

const schema = z.object({ plate: z.string().min(7, 'Informe a placa') });
type FormData = z.infer<typeof schema>;

export default function VehicleCheckout() {
  const { register, handleSubmit, formState: { errors, isSubmitting } } =
    useForm<FormData>({ resolver: zodResolver(schema) });

  const onSubmit = async ({ plate }: FormData) => {
    const res = await checkoutVehicle(plate.toUpperCase());
    alert(`Check‑out OK. Valor a pagar: R$ ${res.price.toFixed(2)}`);
  };

  return (
    <Container maxWidth="sm" sx={{ py: 3 }}>
      <Typography variant="h5" gutterBottom>Check‑out</Typography>
      <Box component="form" onSubmit={handleSubmit(onSubmit)} noValidate>
        <Stack spacing={2}>
          <TextField
            label="Placa"
            {...register('plate')}
            error={!!errors.plate}
            helperText={errors.plate?.message}
            inputProps={{ style: { textTransform: 'uppercase' } }}
          />
          <Button type="submit" variant="contained" disabled={isSubmitting}>
            {isSubmitting ? 'Processando...' : 'Registrar Check‑out'}
          </Button>
          <Alert severity="info">O backend deve calcular o valor e devolver no payload.</Alert>
        </Stack>
      </Box>
    </Container>
  );
}

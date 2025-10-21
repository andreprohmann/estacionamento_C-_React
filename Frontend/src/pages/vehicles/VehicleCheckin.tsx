import { z } from 'zod';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import {
  Alert, Box, Button, Container, Stack, TextField, Typography
} from '@mui/material';
import { checkinVehicle } from '../../services/parkingService';

const plateRegex =/^(?:[A-Z]{3}-?\d{4}|[A-Z]{3}\d[A-Z]\d{2})$/i; // simples; adapte se usar Mercosul
const schema = z.object({
  plate: z.string().min(7, 'Informe a placa').regex(plateRegex, 'Placa inválida'),
  model: z.string().optional(),
  color: z.string().optional(),
  driverName: z.string().min(2, 'Nome muito curto'),
});

type FormData = z.infer<typeof schema>;

export default function VehicleCheckin() {
  const { register, handleSubmit, formState: { errors, isSubmitting }, reset } =
    useForm<FormData>({ resolver: zodResolver(schema) });

  const onSubmit = async (data: FormData) => {
    await checkinVehicle({
      plate: data.plate.toUpperCase(),
      model: data.model,
      color: data.color,
      driverName: data.driverName,
    });
    reset();
    alert('Check-in realizado!');
  };

  return (
    <Container maxWidth="sm" sx={{ py: 3 }}>
      <Typography variant="h5" gutterBottom>Check‑in de Veículo</Typography>
      <Box component="form" onSubmit={handleSubmit(onSubmit)} noValidate>
        <Stack spacing={2}>
          <TextField
            label="Placa"
            {...register('plate')}
            error={!!errors.plate}
            helperText={errors.plate?.message}
            inputProps={{ style: { textTransform: 'uppercase' } }}
          />
          <TextField label="Modelo" {...register('model')} />
          <TextField label="Cor" {...register('color')} />
          <TextField
            label="Nome do motorista"
            {...register('driverName')}
            error={!!errors.driverName}
            helperText={errors.driverName?.message}
          />
          <Button type="submit" variant="contained" disabled={isSubmitting}>
            {isSubmitting ? 'Enviando...' : 'Registrar Check‑in'}
          </Button>
          <Alert severity="info">
            Ajuste as rotas no arquivo <code>parkingService.ts</code> para casar com sua API.
          </Alert>
        </Stack>
      </Box>
    </Container>
  );
}
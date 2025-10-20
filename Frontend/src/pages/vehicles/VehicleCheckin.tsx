
import { z } from 'zod';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { Alert, Box, Button, Container, Stack, TextField, Typography } from '@mui/material';
import { checkinVehicle } from '../../services/parkingService';

// Aceita padrão antigo (ABC-1234/ABC1234) e Mercosul (ABC1D23)
export const plateRegex = /^(?:[A-Z]{3}-?\d{4}|[A-Z]{3}\d[A-Z]\d{2})$/i;

const schema = z.object({
  placa: z.string().regex(plateRegex, 'Placa inválida'),
  modelo: z.string().optional(),
  cor: z.string().optional(),
  nomeMotorista: z.string().min(2, 'Nome muito curto').optional(),
});

type FormData = z.infer<typeof schema>;

export default function VehicleCheckin() {
  const { register, handleSubmit, formState: { errors, isSubmitting }, reset } =
    useForm<FormData>({ resolver: zodResolver(schema) });

  const onSubmit = async (data: FormData) => {
    await checkinVehicle({
      placa: data.placa.toUpperCase().replace('-', ''),
      modelo: data.modelo,
      cor: data.cor,
      nomeMotorista: data.nomeMotorista,
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
            {...register('placa')}
            error={!!errors.placa}
            helperText={errors.placa?.message}
            inputProps={{ style: { textTransform: 'uppercase' } }}
          />
          <TextField label="Modelo" {...register('modelo')} />
          <TextField label="Cor" {...register('cor')} />
          <TextField label="Nome do motorista" {...register('nomeMotorista')} />
          <Button type="submit" variant="contained" disabled={isSubmitting}>
            {isSubmitting ? 'Enviando...' : 'Registrar Check‑in'}
          </Button>

          {/* Corrigido: exibir chaves como texto literal */}
          <Alert severity="info">
            Payload alinhado: {'{ placa, modelo, cor, nomeMotorista }'}
          </Alert>
        </Stack>
      </Box>
    </Container>
  );
}

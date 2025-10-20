
import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    strictPort: false,
    proxy: {
      '/vagas': { target: 'http://localhost:5082', changeOrigin: true },
      '/veiculos': { target: 'http://localhost:5082', changeOrigin: true },
      '/estacionamento': { target: 'http://localhost:5082', changeOrigin: true },
    }
  }
});

import { defineConfig } from 'vite';

export default defineConfig({
  // Otras configuraciones de Vite
  server: {
    hmr: {
      overlay: false
    }
  }
});

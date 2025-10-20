
interface PublicEnv {
  VITE_API_BASE_URL?: string;
  NODE_ENV?: 'development' | 'production' | 'test';
}

const ENV = import.meta.env as unknown as PublicEnv;

export const API_BASE_URL = (ENV.VITE_API_BASE_URL ?? '').trim() || (ENV.NODE_ENV === 'development' ? '' : '');
export const IS_DEV = ENV.NODE_ENV === 'development';


import { Routes, Route, Navigate } from 'react-router-dom';
import NavBar from './components/NavBar';
import Dashboard from './pages/Dashboard';
import VehiclesList from './pages/vehicles/VehiclesList';
import VehicleCheckin from './pages/vehicles/VehicleCheckin';
import VehicleCheckout from './pages/vehicles/VehicleCheckout';

export default function App() {
  return (
    <>
      <NavBar />
      <Routes>
        <Route path="/" element={<Dashboard />} />
        <Route path="/veiculos" element={<VehiclesList />} />
        <Route path="/checkin" element={<VehicleCheckin />} />
        <Route path="/checkout" element={<VehicleCheckout />} />
        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </>
  );
}

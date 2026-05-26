import { Navigate, Route, Routes } from 'react-router-dom'
import Home from './pages/home/HomePage'
import CreateEvent from './pages/createEvent/CreateEvent'
import OrganizerEvents from './pages/organizerEvents/OrganizerEvents'
import Register from "./pages/register/Register";
import Login from "./pages/login/LoginPage";
import Payment from './pages/Payment/PaymentPage';
import './App.css'


function App() {
  return (
    <Routes>
      <Route path='/' element={<Navigate to='/login' replace />} />
      <Route path='/login' element={<Login />} />
      <Route path='/register' element={<Register />} />
      <Route path='/home' element={<Home />} />
      <Route path='/payment' element={<Payment />} />
      <Route path='/create-event' element={<CreateEvent />} />
      <Route path='/organizer' element={<Navigate to='/organizer/events' replace />} />
      <Route path='/organizer/events' element={<OrganizerEvents />} />
      <Route path='/organizerEvents' element={<Navigate to='/organizer/events' replace />} />
      <Route path='*' element={<Navigate to='/login' replace />} />
    </Routes>
  )
}


export default App

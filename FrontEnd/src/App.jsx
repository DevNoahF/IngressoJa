import { Navigate, Route, Routes } from 'react-router-dom'
import Home from './pages/home/HomePage'
import CreateEvent from './pages/createEvent/CreateEvent'
import OrganizerEvents from './pages/organizerEvents/OrganizerEvents'
import Register from "./pages/register/Register"
import Login from "./pages/login/LoginPage"
import Payment from './pages/Payment/PaymentPage'
import UpdateProfile from './pages/updateUser/updateUser'
import ChangeEventStatus from './pages/changeEventStatus/ChangeEventStatusPage'
import './App.css'

function App() {
  return (
    <Routes>
      <Route path='/' element={<Navigate to='/login' replace />} />
      <Route path='/login' element={<Login />} />
      <Route path='/cadastro/usuario' element={<Navigate to='/user/register' replace />} />
      <Route path='/cadastro/organizador' element={<Navigate to='/organizer/register' replace />} />
      <Route path='/user/register' element={<Register />} />
      <Route path='/organizer/register' element={<Register />} />
      <Route path='/user/home' element={<Home />} />
      <Route path='/user/payment' element={<Payment />} />
      <Route path='/organizer/create' element={<CreateEvent />} />
      <Route path='/organizer/home' element={<OrganizerEvents />} />
      <Route path='/update' element={<UpdateProfile />} />
      <Route path='/change-event-status' element={<ChangeEventStatus />} />
      <Route path='*' element={<Navigate to='/user/home' replace />} />
    </Routes>
  )
}

export default App
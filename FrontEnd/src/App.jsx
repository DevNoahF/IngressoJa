import { Navigate, Route, Routes } from 'react-router-dom'
import Home from './pages/home/HomePage'
import CreateEvent from './pages/createEvent/CreateEvent'
import OrganizerEvents from './pages/organizerEvents/OrganizerEvents'
import Register from "./pages/register/Register";
import Login from "./pages/login/LoginPage";
import Payment from './pages/Payment/PaymentPage';
import './App.css'
import UpdateProfile from './pages/updateUser/updateUser';


function App() {
  return (
    <Routes>
      <Route path='/' element={<Navigate to='/login' replace />} />
      <Route path='/login' element={<Login />} />
      <Route path='/user/register' element={<Register />} />
      <Route path='/organizer/register' element={<Register />} />
      <Route path='/user/home' element={<Home />} />
      <Route path='/user/payment' element={<Payment />} />
      <Route path='/organizer/create' element={<CreateEvent />} />
      <Route path='/organizer/home' element={<OrganizerEvents />} />
      <Route path='/update' element={<UpdateProfile />} />
    </Routes>
  )
}


export default App

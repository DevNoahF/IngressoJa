import { Navigate, Route, Routes } from 'react-router-dom'
import Home from './pages/home/HomePage'
import CreateEvent from './pages/createEvent/CreateEvent'
import OrganizerEvents from './pages/organizerEvents/OrganizerEvents'
import RegisterUser from './pages/registerUser/RegisterUser'
import RegisterOrganizer from './pages/registerOrganizer/registerOrganizer'
import Login from "./pages/login/LoginPage"
import Payment from './pages/Payment/PaymentPage'
import PurchasesPage from './pages/purchases/PurchasesPage'
import TicketsPage from './pages/tickets/TicketsPage'
import UpdateProfile from './pages/updateUser/updateUser'
import ChangeEventStatus from './pages/changeEventStatus/ChangeEventStatusPage'
import './App.css'

function App() {
  return (
    <Routes>
      <Route path='/' element={<Navigate to='/login' replace />} />
      <Route path='/login' element={<Login />} />
      <Route path='/user/register' element={<RegisterUser />} />
      <Route path='/organizer/register' element={<RegisterOrganizer />} />
      <Route path='*' element={<Navigate to='/user/home' replace />} />
      <Route path='/user/home' element={<Home />} />
      <Route path='/user/payment' element={<Payment />} />
      <Route path='/user/purchases' element={<PurchasesPage />} />
      <Route path='/user/tickets' element={<TicketsPage />} />
      <Route path='/organizer/create' element={<CreateEvent />} />
      <Route path='/organizer/home' element={<OrganizerEvents />} />
      <Route path='/update' element={<UpdateProfile />} />
    </Routes>
  )
}

export default App
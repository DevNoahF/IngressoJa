import { Navigate, Route, Routes } from 'react-router-dom'
import ChangeEventStatus from './pages/changeEventStatus/ChangeEventStatusPage'
import Home from './pages/home/HomePage'
import CreateEvent from './pages/createEvent/CreateEvent'
import Register from "./pages/register/Register"
import Login from "./pages/login/LoginPage"
import Payment from './pages/Payment/PaymentPage'
import './App.css'
import { canCreateEvent } from './utils/auth'
import UpdateProfile from './pages/updateUser/updateUser'
import OrganizerEvents from './pages/organizerEvents/OrganizerEvents'

function RequireCreateEventAccess({ children }) {
  if (!canCreateEvent()) {
    return <Navigate to='/' replace />
  }
  return children
}

function App() {
  return (
    <Routes>
      <Route path='/' element={<Navigate to='/login' replace />} />
      <Route path='/login' element={<Login />} />
      <Route path='/register' element={<Register />} />
      <Route path='/home' element={<Home />} />
      <Route path='/user/home' element={<Home />} />
      <Route path='/payment' element={<Payment />} />
      <Route path='/user/payment' element={<Payment />} />
      <Route path='/change-event-status' element={<ChangeEventStatus />} />
      <Route path='/create-event' element={
        <RequireCreateEventAccess>
          <CreateEvent />
        </RequireCreateEventAccess>
      } />
      <Route path='/organizer/create' element={<CreateEvent />} />
      <Route path='/organizer/home' element={<OrganizerEvents />} />
      <Route path='/update' element={<UpdateProfile />} />
      <Route path='*' element={<Navigate to='/login' replace />} />
    </Routes>
  )
}

export default App
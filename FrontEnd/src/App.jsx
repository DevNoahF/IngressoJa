import { Navigate, Route, Routes } from 'react-router-dom'
import Home from './pages/home/HomePage'
import CreateEvent from './pages/createEvent/CreateEvent'
import OrganizerEvents from './pages/organizerEvents/OrganizerEvents'
import Register from "./pages/register/Register";
import Login from "./pages/login/LoginPage";
import Payment from './pages/Payment/PaymentPage';
import './App.css'
import { canCreateEvent } from './utils/auth'

function RequireOrganizerAccess({ children }) {
  return children
}


function App() {
  return (
    <Routes>
      <Route path='/login' element={<Login />} />
      <Route path='/register' element={<Register />} />
      <Route path='/home' element={<Home />} />
      <Route path='/payment' element={<Payment />} />
      <Route
        path='/create-event'
        element={(
          <RequireOrganizerAccess>
            <CreateEvent />
          </RequireOrganizerAccess>
        )}
      />
      <Route
        path='/organizer/events'
        element={(
          <RequireOrganizerAccess>
            <OrganizerEvents />
          </RequireOrganizerAccess>
        )}
      />
      <Route path='*' element={<Navigate to='/login' replace />} />
    </Routes>
  )
}


export default App

import { Navigate, Route, Routes } from 'react-router-dom'
import Home from './pages/HomePage'
import CreateEvent from './pages/createEvent/CreateEvent'
import Register from "./pages/register/Register";
import './App.css'
import { canCreateEvent } from './utils/auth'

function RequireCreateEventAccess({ children }) {
  if (!canCreateEvent()) {
    return <Navigate to='/' replace />
  }

  return children
}


function App() {
  return (
    <Routes>
      <Route path='/register' element={<Register />} />
      <Route path='/' element={<Home />} />
      <Route
        path='/create-event'
        element={(
          <RequireCreateEventAccess>
            <CreateEvent />
          </RequireCreateEventAccess>
        )}
      />
      <Route path='*' element={<Navigate to='/' replace />} />
    </Routes>
  )
}


export default App

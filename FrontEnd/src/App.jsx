import { Navigate, Route, Routes } from 'react-router-dom'
import Home from './pages/HomePage'
import CreateEvent from './pages/createEvent/CreateEvent'
import Register from "./pages/register/Register";
import './App.css'


function App() {
  return (
    <Routes>
      <Route path='/register' element={<Register />} />
      <Route path='/' element={<Home />} />
      <Route path='/create-event' element={<CreateEvent />} />
      <Route path='*' element={<Navigate to='/' replace />} />
    </Routes>
  )
}


export default App

import { Navigate, Route, Routes } from 'react-router-dom'
import Home from './pages/home/HomePage'
import './App.css'
import LoginPage from './pages/login/LoginPage'


function App() {
  return (
    <Routes>
      <Route path='/' element={<Home />} />
      <Route path='/login' element={<LoginPage />} />
      <Route path='*' element={<Navigate to='/' replace />} />
    </Routes>
  )
}


export default App

import { Navigate, Route, Routes } from 'react-router-dom'
import Home from './pages/HomePage'
import './App.css'


function App() {
  return (
    <Routes>
      <Route path='/' element={<Home />} />
      <Route path='*' element={<Navigate to='/' replace />} />
    </Routes>
  )
}


export default App

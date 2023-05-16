import './App.css'
import LoginPage from './pages/LoginPage'
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import OrdersPage from './pages/OrdersPage'
import NavBarPage from './pages/NavBarPage';


function App() {

  return (
    <div className="flex flex-row h-screen w-screen m-0 p-0 bg-red-500">
        <BrowserRouter>
        <Routes>
          <Route path="/delivery/" element={<><NavBarPage/><LoginPage /></>}/>
          <Route path="/delivery/orders" element={<><NavBarPage/><OrdersPage/></>}/> 
        </Routes>
      </BrowserRouter>
      </div>
  )
}

export default App
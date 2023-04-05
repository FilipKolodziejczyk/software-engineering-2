import './App.css'
import NavBarPage from './pages/NavBarPage'
import LoginPage from './pages/LoginPage'
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import ProductsPage from './pages/ProductsPage'
import OrdersPage from './pages/OrdersPage'
import ComplaintsPage from './pages/ComplaintsPage'
import ProductItemAddPage from './pages/ProductItemAddPage';

function App() {

  return (
    <div className="flex flex-row h-screen w-screen m-0 p-0 bg-red-500">
        <BrowserRouter>
        <Routes>
          <Route path="/" element={<><NavBarPage/><LoginPage /></>}/>
          <Route path="/products" element={<><NavBarPage/><ProductsPage/></>}/> 
          <Route path="/products/new" element={<><NavBarPage/><ProductItemAddPage/></>}/> 
          <Route path="/orders" element={<><NavBarPage/><OrdersPage/></>}/> 
          <Route path="/complaints" element={<><NavBarPage/><ComplaintsPage/></>}/> 
        </Routes>
      </BrowserRouter>
      </div>
  )
}

export default App

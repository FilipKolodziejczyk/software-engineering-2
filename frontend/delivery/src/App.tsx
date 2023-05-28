import './App.css'
import LoginPage from './pages/LoginPage'
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import OrdersPage from './pages/OrdersPage'
import NavBarPage from './pages/NavBarPage';
import { createContext, useState } from 'react';

export type UserAttributes = {
  token: String
  setToken:(t: String) => void
}

export const Context = createContext<UserAttributes>({
token: '',
setToken: () => {},
});

function App() {
  const [token, setToken] = useState<String>('')

  return (
    <Context.Provider value={{ token, setToken}}>
    <div className="flex flex-row h-screen w-screen m-0 p-0 bg-red-500">
        <BrowserRouter>
        <Routes>
          <Route path="/delivery/" element={<><NavBarPage/><LoginPage /></>}/>
          <Route path="/delivery/orders" element={<><NavBarPage/><OrdersPage/></>}/> 
        </Routes>
      </BrowserRouter>
    </div>
    </Context.Provider>
  )
}

export default App
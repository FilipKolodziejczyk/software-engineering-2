import React from 'react'
import ReactDOM from 'react-dom/client'
import {createBrowserRouter, RouterProvider,} from "react-router-dom";
import './index.css'
import Root from "./pages/Root";
import ErrorPage from "./pages/ErrorPage";
import ProductsPage from "./pages/ProductsPage";
import LoginPage from "./pages/LoginPage";
import ProductDetailsPage, {loader as productLoader} from "./pages/ProductDetailsPage";
import CartOverviewPage from "./pages/CartOverviewPage";
// import CheckoutPage from "./pages/CheckoutPage";
import {AuthProvider} from "./context/AuthContext";
import {ProtectedRoute} from "./pages/ProtectedRoute";
import LogoutPage from "./pages/LogoutPage";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Root/>,
    errorElement: <ErrorPage/>,
    children: [
      {
        path: "/",
        element: <ProductsPage/>
      },
      {
        path: "products/:id",
        element: <ProductDetailsPage/>,
        loader: productLoader,
      },
      {
        path: "cart",
        element: <CartOverviewPage/>
      },
      // {
      //   path: "checkout",
      //   element: <CheckoutPage/>
      // }
    ]
  },
  {
    path: "login",
    element: <LoginPage/>
  },
  {
    path: "logout",
    element: <LogoutPage/>
  },
  {
    element: <ProtectedRoute/>,
    children: [

    ]
  }
]);

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <React.StrictMode>
    <AuthProvider>
      <RouterProvider router={router}/>
    </AuthProvider>
  </React.StrictMode>
)

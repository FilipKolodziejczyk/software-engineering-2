import React from 'react'
import ReactDOM from 'react-dom/client'
import {createBrowserRouter, RouterProvider,} from "react-router-dom";
import './index.css'
import Root from "./pages/Root";
import ErrorPage from "./pages/ErrorPage";
import ProductsPage, {loader as productListLoader} from "./pages/ProductsPage";
import LoginPage from "./pages/LoginPage";
import ProductDetailsPage, {loader as productLoader} from "./pages/ProductDetailsPage";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Root/>,
    errorElement: <ErrorPage/>,
    children: [
      {
        path: "/",
        element: <ProductsPage/>,
        loader: productListLoader,
      },
      {
        path: "login",
        element: <LoginPage/>
      },
      {
        path: "products/:id",
        element: <ProductDetailsPage/>,
        loader: productLoader,
      }
    ]
  },
]);

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <React.StrictMode>
    <RouterProvider router={router}/>
  </React.StrictMode>
)

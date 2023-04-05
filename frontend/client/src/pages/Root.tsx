import {ShoppingBagIcon} from "@heroicons/react/24/outline";
import {Link, Outlet} from "react-router-dom";

export default function Root() {
  return (
    <div className="App">
      <header>
        <div className="bg-white border-gray-200 border px-4 lg:px-6 py-4">
          <div className="flex flex-wrap justify-between items-center">
            <Link to="/" className="flex items-center">
              <span className="self-center text-xl font-light whitespace-nowrap">Flower Shop</span>
            </Link>
            <div className="flex items-center gap-4">
              <Link to="/cart"><ShoppingBagIcon className="w-6 h-6"/></Link>
              <Link to="/login">Login</Link>
              <Link to="/register">Register</Link>
            </div>
          </div>
        </div>
      </header>
      <main className="flex flex-col gap-4">
        <Outlet/>
      </main>

    </div>
  )
}
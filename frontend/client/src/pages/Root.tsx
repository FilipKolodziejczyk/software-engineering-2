import {ShoppingBagIcon, UserIcon} from "@heroicons/react/24/outline";
import {Link, Outlet} from "react-router-dom";
import {MagnifyingGlassIcon} from "@heroicons/react/24/solid";

export default function Root() {
  return (<div className="App">
    <nav className="sticky top-0 z-20">
      <div className="bg-white border-gray-200 border px-4 lg:px-6 py-4">
        <div className="flex flex-wrap justify-between items-center">
          <Link to="/" className="flex items-center">
            <span className="self-center text-xl font-bold whitespace-nowrap mr-4">Flower Shop</span>
          </Link>
          <div className="flex items-center gap-4">
            <div
              className="relative sm:w-48 h-10 flex items-center sm:rounded-full sm:border sm:border-gray-200 sm:px-4 sm:py-2">
              <MagnifyingGlassIcon className="w-6 h-6"/>
              <input type="text" placeholder="Search"
                     className="hidden sm:block w-full h-full form-input bg-transparent border-0 focus:ring-0 outline-none text-sm text-gray-700 placeholder-gray-500 pl-2"/>
            </div>
            <Link to="/cart"><ShoppingBagIcon className="w-6 h-6"/></Link>
            <Link to="/login"><UserIcon className="w-6 h-6"/></Link>
          </div>
        </div>
      </div>
    </nav>
    <main className="flex flex-col gap-4">
      <Outlet/>
    </main>

  </div>)
}
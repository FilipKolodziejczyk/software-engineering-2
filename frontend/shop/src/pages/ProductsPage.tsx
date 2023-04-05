import { useState } from "react";
import { Link } from "react-router-dom";
import ProductItem from "../components/ProductItem";
import ProductItemAdd from "./ProductItemAddPage";

function ProductsPage  ()  {  

    return (
    
    <div className="flex flex-col items-center justify-center h-screen w-5/6 p-4 bg-gray-300">
        <div className="flex justify-end absolute top-2 right-4">
            <Link to="/products/new">
            <button className="bg-gray-100 text-gray-950 py-2 px-4 rounded hover:bg-gray-300 font-medium py-2 px-4">
                add
            </button>
            </Link>
            <input className="bg-gray-100 text-gray-950 ml-2 rounded" type="text" placeholder="search" />
        </div>

        <div className="h-full overflow-y-auto w-full mt-10">
            <ProductItem/>
            <ProductItem/>
            <ProductItem/>
        </div>
    </div>
    )
};

export default ProductsPage;
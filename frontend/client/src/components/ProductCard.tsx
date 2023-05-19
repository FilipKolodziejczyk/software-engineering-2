import React from 'react';
import {Product} from "../models/Product";
import {Link} from "react-router-dom";

export default function ProductCard({product}: { product: Product }) {
  return (<Link className="group relative" to={`/products/${product.productID}`}>
    <div
      className="min-h-80 aspect-h-1 aspect-w-1 w-full overflow-hidden rounded-xl bg-gray-200 border border-gray-200 lg:aspect-none group-hover:opacity-75 lg:h-80">
      <img src={product.image}
           alt={product.name}
           className="h-full w-full object-cover object-center lg:h-full lg:w-full cursor-pointer text-gray-200"/>
    </div>
    <h3 className="mt-4 text-sm text-gray-700">{product.name}</h3>
    <p className="mt-1 text-lg font-medium text-gray-900">${product.price}</p>
  </Link>);
}
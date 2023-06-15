import React from 'react';
import {Product} from "../models/Product";
import {Link} from "react-router-dom";
import {currencyFormat} from "../utilities/currencyFormat";

export default function ProductCard({product}: { product: Product }) {
  return (<Link className="group relative" to={`/products/${product.productID}`}>
    <div
      className="min-h-80 aspect-h-1 aspect-w-1 w-full overflow-hidden rounded-xl bg-gray-200 border border-gray-200 lg:aspect-none group-hover:opacity-75 lg:h-80 transition-all">
      <img src={product.imageUris[0]}
           alt={product.name}
           className="h-full w-full object-cover object-center lg:h-full lg:w-full cursor-pointer text-gray-200 aspect-[3/4]"/>
    </div>
    <h3 className="mt-4 text-sm text-gray-700">{product.name}</h3>
    <p className="mt-1 text-lg font-medium text-gray-900">{currencyFormat(product.price)}</p>
  </Link>);
}

export function ProductCardSkeleton() {
  return (<div className="group relative">
    <div
      className="min-h-80 aspect-h-1 aspect-w-1 w-full overflow-hidden rounded-xl bg-gray-200 border border-gray-200 lg:aspect-none group-hover:opacity-75 lg:h-80">
      <div
        className="animate-pulse h-full w-full object-cover object-center lg:h-full lg:w-full cursor-pointer text-gray-200 aspect-[3/4]"/>
    </div>
    <h3 className="mt-4 text-sm text-gray-700">
      <div className="animate-pulse w-1/2 h-4 bg-gray-200 rounded-sm"/>
    </h3>
    <span className="mt-1 text-lg font-medium text-gray-900">
      <div className="animate-pulse w-1/4 h-4 bg-gray-200 rounded-sm"/>
    </span>
  </div>);
}
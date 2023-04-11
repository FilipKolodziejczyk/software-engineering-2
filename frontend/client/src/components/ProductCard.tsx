import React from 'react';
import {Product} from "../models/Product";

export default function ProductCard({product}: { product: Product }) {
  return (
    <div className="group relative">
      <div
        className="min-h-80 aspect-h-1 aspect-w-1 w-full overflow-hidden rounded-xl bg-gray-200 border border-gray-200 lg:aspect-none group-hover:opacity-75 lg:h-80">
        <img src={`https://source.unsplash.com/random/900×700/?bouquet&sig=${product.id}`}
             alt={product.title}
             className="h-full w-full object-cover object-center lg:h-full lg:w-full cursor-pointer"/>
      </div>
      <h3 className="mt-4 text-sm text-gray-700">{product.title}</h3>
      <p className="mt-1 text-lg font-medium text-gray-900">${product.price}</p>
    </div>
  );
}
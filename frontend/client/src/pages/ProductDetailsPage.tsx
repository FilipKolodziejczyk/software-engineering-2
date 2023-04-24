import {useLoaderData} from "react-router-dom";
import {Product} from "../models/Product";
import React from "react";
import {CounterInput} from "../components/CounterInput";

export async function loader({params}: any) {
  const product: Product = await fetch(`https://fakestoreapi.com/products/${params.id}`).then(res => res.json());
  return {product};
}

export default function ProductDetailsPage() {
  const [quantity, setQuantity] = React.useState(1);

  const {product}: any = useLoaderData();
  return (
    <div className="mx-auto max-w-2xl px-4 py-4 pb-12 sm:px-6 sm:py-8 lg:max-w-5xl 2xl:max-w-7xl lg:px-8">
      <div className="md:grid md:grid-cols-2 md:gap-x-16 md:items-start">
        <div className="mb-4">
          <div className="w-full md:h-full p-2 md:p-0">
            <img src={`https://source.unsplash.com/random/900×700/?bouquet&sig=${product.id}`}
                 alt={product.title}
                 className="h-full w-full object-cover object-center md:h-full md:w-full rounded-xl"/>

          </div>
        </div>
        <div>
          <h3 className="text-3xl font-extrabold tracking-tight text-gray-900">{product.title}</h3>
          <p className="mt-3 text-xl text-gray-500">${product.price}</p>
          <div className="mt-6">
            <h3 className="sr-only">Description</h3>
            <div className="text-base text-gray-700 space-y-6">
              {product.description}
            </div>
          </div>
          <div className="mt-10">
            <div className="flex flex-row">

              <div className="flex flex-row mr-4">
                <CounterInput
                  value={quantity}
                  onDecrease={() => {
                    if (quantity > 0) setQuantity(quantity - 1)
                  }}
                  onChange={(e) => {
                    const value = parseInt(e.target.value);
                    if (value >= 0)
                      setQuantity(value)
                  }}
                  onIncrease={() => setQuantity(quantity + 1)}/>
              </div>


              <button type="button"
                      className="inline-flex items-center px-6 py-3 border border-transparent text-base font-medium rounded-md shadow-sm text-white bg-gray-900 hover:bg-gray-800 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-900">
                Add to cart
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
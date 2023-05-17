import {useLoaderData} from "react-router-dom";
import {Product} from "../models/Product";
import React from "react";
import {CounterInput} from "../components/CounterInput";
import {ImageCarousel} from "../components/ImageCarousel";

export async function loader({params}: any) {
  const product: Product = await fetch(`${import.meta.env.VITE_API_BASE_URL}/api/products/${params.id}`).then(res => res.json());
  return {product};
}

export default function ProductDetailsPage() {
  const [quantity, setQuantity] = React.useState(1);

  const {product}: any = useLoaderData();
  return (<div className="mx-auto max-w-2xl px-4 py-4 pb-12 sm:px-6 sm:py-8 lg:max-w-5xl 2xl:max-w-7xl lg:px-8">
    <div className="lg:grid lg:grid-cols-2 lg:gap-x-16 lg:items-start">
      <div className="mb-4">
        <ImageCarousel imageSources={product.image}/>
      </div>
      <div>
        <h3 className="text-2xl sm:text-3xl font-bold tracking-tight text-gray-900">{product.name}</h3>
        <p className="mt-3 text-xl sm:text-3xl tracking-tight text-gray-900">${product.price}</p>
        <div className="mt-6">
          <h3 className="sr-only">Description</h3>
          <p className="text-base text-gray-700 space-y-6">
            {product.description}
          </p>
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
                  if (value >= 0) setQuantity(value)
                }}
                onIncrease={() => setQuantity(quantity + 1)}/>
            </div>


            <button type="button"
                    className="inline-flex items-center px-6 py-3 border border-transparent text-base font-medium rounded-md shadow-sm text-white bg-neutral-950 hover:bg-neutral-900 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-900">
              Add to cart
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>);
}
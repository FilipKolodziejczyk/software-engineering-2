import {useShoppingCart} from "../context/ShoppingCartContext";
import React from "react";
import OrderSummary from "../components/OrderSummary";
import CartItemPanel from "../components/CartItemPanel";


export default function CartOverviewPage() {
  const {items, removeItem, updateItem} = useShoppingCart();

  return (
    <div>
      <main className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
        <div className="flex items-baseline justify-between border-b border-gray-200 pb-6 pt-12">
          <h1 className="text-2xl sm:text-4xl font-bold tracking-tight text-gray-900">Shopping Cart</h1>
        </div>

        <div className="my-12 sm:grid sm:grid-cols-12 gap-8">
          <div className="col-span-12 sm:col-span-7 -mt-4">
            <ul>
              {items.map((item, index) =>
                <CartItemPanel key={index} item={item} removeItem={removeItem} updateItem={updateItem}/>
              )}
            </ul>
          </div>
          <div className="col-span-12 sm:col-span-5">
            <div className="sticky top-24">
              <OrderSummary items={items}/>
            </div>
          </div>
        </div>
      </main>
    </div>
  );
}
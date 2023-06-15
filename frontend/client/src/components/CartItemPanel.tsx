import {CartItem} from "../models/CartItem";
import {XMarkIcon} from "@heroicons/react/24/outline";
import React from "react";
import {CounterInput} from "./CounterInput";
import {currencyFormat} from "../utilities/currencyFormat";

type CartItemPanelProps = {
  item: CartItem, removeItem: (item: CartItem) => void, updateItem: (item: CartItem) => void
}
export default function CartItemPanel({item, removeItem, updateItem}: CartItemPanelProps) {
  const updateQuantity = (quantity: number) => {
    updateItem({...item, quantity})
  }

  return (<li className="py-8 flex odd:border-t odd:border-b last:border-b border-gray-200">
    <div className="shrink-0">
      <img className="h-32 w-32 md:h-48 md:w-48 rounded-md object-cover" src={item.product.imageUris[0]}
           alt={item.product.name}/>
    </div>
    <div className="flex flex-col ml-8 justify-between flex-1">
      <div className="flex flex-col h-full">
        <div className="flex justify-between text-base font-medium text-gray-900">
          {item.product.name}
          <XMarkIcon
            className="h-5 w-5 text-gray-400 hover:text-gray-500 cursor-pointer"
            onClick={() => removeItem(item)}
          />
        </div>
        <p className="mt-2 text-sm text-gray-500">
          {currencyFormat(item.product.price)}
        </p>
        <div className="mt-auto justify-end flex">
          <CounterInput value={item.quantity} onChange={
            (e) => {
              const value = parseInt(e.target.value);
              if (value >= 0) updateQuantity(value)
            }
          } onDecrease={() => updateQuantity(item.quantity - 1)} onIncrease={() => updateQuantity(item.quantity + 1)}/>
        </div>
      </div>
    </div>
  </li>);
}

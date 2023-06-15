import React, {Fragment} from 'react';
import {Link} from "react-router-dom";
import {CartItem} from "../models/CartItem";
import {Popover, Transition} from '@headlessui/react'
import {ShoppingBagIcon, XMarkIcon} from "@heroicons/react/24/outline";
import {useShoppingCart} from "../context/ShoppingCartContext";
import {currencyFormat} from "../utilities/currencyFormat";

export default function CartPopover() {
  const {items, removeItem} = useShoppingCart()
  return (<Popover className="relative h-6">
    {({open}) => (<>
      <Popover.Button
        className={`
                ${open ? '' : 'text-opacity-90'}
                group inline-flex items-center rounded-md text-base font-medium hover:text-opacity-100 focus:outline-none focus-visible:ring-2 focus-visible:ring-white focus-visible:ring-opacity-75`}
      >
        <ShoppingBagIcon
          className={`${open ? '' : 'text-opacity-70'}
                  h-6 w-6 transition duration-150 ease-in-out group-hover:text-opacity-80`}
          aria-hidden="true"
        />
        <div
          className="absolute inline-flex items-center justify-center w-4 h-4 text-2xs right-1/2 bottom-1/2 translate-x-1/2 translate-y-2/3">
          {items.length}
        </div>
      </Popover.Button>

      <Popover.Overlay className="fixed inset-0 bg-black opacity-30"/>
      <Transition
        as={Fragment}
        enter="transition ease-out duration-200"
        enterFrom="opacity-0 translate-y-1"
        enterTo="opacity-100 translate-y-0"
        leave="transition ease-in duration-150"
        leaveFrom="opacity-100 translate-y-0"
        leaveTo="opacity-0 translate-y-1"
      >
        <Popover.Panel
          className="fixed right-0 z-10 mt-3 w-screen sm:max-w-sm sm:right-4 transform px-4 sm:px-0 lg:max-w-sm ">
          <div className="overflow-hidden rounded-lg shadow-lg ring-1 ring-black ring-opacity-5">
            {items.length > 0 ? (<>
              <div className="relative grid gap-8 bg-white p-7 overflow-y-auto max-h-96">
                {items.map((item: CartItem) => (<a
                  key={item.product.name}
                  className="group -m-3 flex items-center rounded-lg p-2 transition duration-150 ease-in-out hover:bg-gray-50 focus:outline-none focus-visible:ring focus-visible:ring-orange-500 focus-visible:ring-opacity-50"
                >
                  <div className="flex h-10 w-10 shrink-0 items-center justify-center text-white sm:h-14 sm:w-14">
                    <img src={item.product.imageUris[0]} alt={item.product.name} className="h-full w-full rounded-lg"/>
                  </div>
                  <div className="ml-4">
                    <p className="text-md font-medium text-gray-900">
                      {item.product.name}
                    </p>
                    <p className="text-sm text-gray-500">
                      {currencyFormat(item.product.price)} x {item.quantity}
                    </p>
                  </div>
                  <div className="hidden group-hover:block ml-auto mr-2">
                    <XMarkIcon
                      className="h-5 w-5 text-gray-400 hover:text-gray-500 cursor-pointer"
                      onClick={() => removeItem(item)}
                    />
                  </div>
                </a>))}
              </div>
              <div className="bg-gray-50 p-4">
                <Popover.Button as={Link} to={'checkout'}>
                        <span className="flex items-center justify-center bg-gray-950 p-2 rounded-lg my-4 mx-2">
                          <span className="text-sm font-medium text-white">
                            Checkout
                          </span>
                        </span>
                </Popover.Button>
                <Popover.Button as={Link} to={'cart'}>
                        <span className="flex items-center justify-center">
                          <span className="text-sm font-medium text-gray-900 my-2">
                            View Cart
                          </span>
                        </span>
                </Popover.Button>
              </div>
            </>) : (<div className="relative bg-white p-7">
              <p className="text-sm w-full text-center">
                Your cart is empty
              </p>
            </div>)}
          </div>
        </Popover.Panel>
      </Transition>
    </>)}
  </Popover>);
}
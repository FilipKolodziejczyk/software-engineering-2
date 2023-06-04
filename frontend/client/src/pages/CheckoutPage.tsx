import React from "react";
import {useShoppingCart} from "../context/ShoppingCartContext";
import {Form, Link} from "react-router-dom";
import {currencyFormat} from "../utilities/currencyFormat";

export default function CheckoutPage() {
  const {items} = useShoppingCart();
  const subtotal = items.reduce((total, item) => total + item.product.price * item.quantity, 0);

  return (<div className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8 w-full">
      <div className="flex items-baseline justify-between border-b border-gray-200 pb-6 pt-12">
        <h1 className="text-2xl sm:text-4xl font-bold tracking-tight text-gray-900">Checkout</h1>
        <Link to="/cart"
              className="text-sm font-medium text-neutral-900 hover:text-neutral-700 transition-all"
        >
          Return to cart
        </Link>
      </div>
      <div className="mb-8 grid grid-cols-2 gap-8">
        {/*  payment and shipping details */}
        <Form className="py-8 px-4">
          <div className="mx-auto max-w-none">
            <section>
              <h2 className="text-gray-900 text-lg font-medium">
                Contact information
              </h2>
              <div className="mt-4">
                <label htmlFor="email-address" className="text-gray-500 font-medium text-sm">Email address</label>
                <div className="mt-1">
                  <input type="email"
                         className="text-sm shadow-sm focus:ring-neutral-900 focus:border-neutral-900 block w-full border-gray-300 rounded-md transition-colors"
                         id="email-address" name="email-address" autoComplete="email"/>
                </div>
              </div>
            </section>
            <section className="mt-8">
              <h2 className="text-gray-900 text-lg font-medium">
                Shipping address
              </h2>
              <div className="mt-4">
                <label htmlFor="country" className="text-gray-500 font-medium text-sm">Country</label>
                <div className="mt-1">
                  <input type="text"
                         className="text-sm shadow-sm focus:ring-neutral-900 focus:border-neutral-900 block w-full border-gray-300 rounded-md transition-colors"
                         id="country" name="country" autoComplete="country"/>
                </div>
              </div>
              <div className="mt-4 grid grid-cols-7 gap-4">
                <div className="col-span-5">
                  <label htmlFor="street-address" className="text-gray-500 font-medium text-sm">Street</label>
                  <div className="mt-1">
                    <input type="text"
                           className="text-sm shadow-sm focus:ring-neutral-900 focus:border-neutral-900 block w-full border-gray-300 rounded-md transition-colors"
                           id="street-address" name="street-address" autoComplete="street-address"/>
                  </div>
                </div>
                <div className="col-span-1">
                  <label htmlFor="street-number" className="text-gray-500 font-medium text-sm">Number</label>
                  <div className="mt-1">
                    <input type="text"
                           className="text-sm shadow-sm focus:ring-neutral-900 focus:border-neutral-900 block w-full border-gray-300 rounded-md transition-colors"
                           id="street-number" name="street-number" autoComplete="street-number"/>
                  </div>
                </div>
                <div className="col-span-1">
                  <label htmlFor="apt-number" className="text-gray-500 font-medium text-sm">Apt.</label>
                  <div className="mt-1">
                    <input type="text"
                           className="text-sm shadow-sm focus:ring-neutral-900 focus:border-neutral-900 block w-full border-gray-300 rounded-md transition-colors"
                           id="apt-number" name="apt-number" autoComplete="apt-number"/>
                  </div>
                </div>
              </div>
              <div className="mt-4 flex gap-4">
                <div>
                  <label htmlFor="city" className="text-gray-500 font-medium text-sm">City</label>
                  <div className="mt-1">
                    <input type="text"
                           className="text-sm shadow-sm focus:ring-neutral-900 focus:border-neutral-900 block w-full border-gray-300 rounded-md transition-colors"
                           id="city" name="city" autoComplete="city"/>
                  </div>
                </div>
                <div>
                  <label htmlFor="state" className="text-gray-500 font-medium text-sm">State/Province</label>
                  <div className="mt-1">
                    <input type="text"
                           className="text-sm shadow-sm focus:ring-neutral-900 focus:border-neutral-900 block w-full border-gray-300 rounded-md transition-colors"
                           id="state" name="state" autoComplete="state"/>
                  </div>
                </div>
                <div>
                  <label htmlFor="postal-code" className="text-gray-500 font-medium text-sm">Postal code</label>
                  <div className="mt-1">
                    <input type="text"
                           className="text-sm shadow-sm focus:ring-neutral-900 focus:border-neutral-900 block w-full border-gray-300 rounded-md transition-colors"
                           id="postal-code" name="postal-code" autoComplete="postal-code"/>
                  </div>
                </div>
              </div>
            </section>
            <section className="mt-8">
              <h2 className="text-gray-900 text-lg font-medium">
                Payment details
              </h2>
              <div className="mt-4">
                <label htmlFor="card-number" className="text-gray-500 font-medium text-sm">Card number</label>
                <div className="mt-1">
                  <input type="text"
                         className="text-sm shadow-sm focus:ring-neutral-900 focus:border-neutral-900 block w-full border-gray-300 rounded-md transition-colors"
                         id="card-number" name="card-number" autoComplete="card-number"/>
                </div>
              </div>
              <div className="mt-4 grid grid-cols-7 gap-4">
                <div className="col-span-5">
                  <label htmlFor="card-holder" className="text-gray-500 font-medium text-sm">Card holder</label>
                  <div className="mt-1">
                    <input type="text"
                           className="text-sm shadow-sm focus:ring-neutral-900 focus:border-neutral-900 block w-full border-gray-300 rounded-md transition-colors"
                           id="card-holder" name="card-holder" autoComplete="card-holder"/>
                  </div>
                </div>
                <div className="col-span-1">
                  <label htmlFor="card-expiration" className="text-gray-500 font-medium text-sm">Expiration</label>
                  <div className="mt-1">
                    <input type="text"
                           className="text-sm shadow-sm focus:ring-neutral-900 focus:border-neutral-900 block w-full border-gray-300 rounded-md transition-colors"
                           id="card-expiration" name="card-expiration" autoComplete="card-expiration"/>
                  </div>
                </div>
                <div className="col-span-1">
                  <label htmlFor="card-cvc" className="text-gray-500 font-medium text-sm">CVC</label>
                  <div className="mt-1">
                    <input type="text"
                           className="text-sm shadow-sm focus:ring-neutral-900 focus:border-neutral-900 block w-full border-gray-300 rounded-md transition-colors"
                           id="card-cvc" name="card-cvc" autoComplete="card-cvc"/>
                  </div>
                </div>
              </div>
            </section>
            <div className="mt-8">
              <button type="submit"
                      className="w-full flex justify-center py-4 px-6 border border-transparent rounded-md shadow-sm text-base font-medium text-white bg-neutral-900 hover:bg-neutral-800 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-neutral-900 transition-all">
                Continue
              </button>
            </div>
          </div>
        </Form>
        <div className="py-8 px-4">
          <div className="p-4 flex flex-col bg-gray-50 rounded-lg">
            <ul className="p-4">
              {items.map((item, index) => (<li key={index}
                                               className="border-b last:border-b-0 border-gray-200 py-4 px-4 flex items-center justify-between">
                  <div className="shrink-0">
                    <img src={item.product.image} alt={item.product.name}
                         className="w-20 h-20 rounded-lg object-cover"/>
                  </div>
                  <div className="flex-1 min-w-0 ml-4">
                    <h3 className="text-sm font-medium text-gray-900 truncate">{item.product.name}</h3>
                    <p className="text-sm text-gray-500 truncate">{item.product.description}</p>
                    <p
                      className="text-sm font-medium text-gray-900 truncate">{currencyFormat(item.product.price)} x {item.quantity}</p>
                  </div>
                </li>))}
            </ul>
          </div>
          {/*  subtotal */}
          <div className="mt-8 px-4">
            <div className="flex justify-between items-center">
              <h2 className="text-gray-900 text-lg font-medium">
                Subtotal
              </h2>
              <p className="text-gray-900 font-medium">{currencyFormat(subtotal)}</p>
            </div>
          </div>
        </div>
      </div>
    </div>);
}
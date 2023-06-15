import {CartItem} from "../models/CartItem";
import {Link} from "react-router-dom";
import {currencyFormat} from "../utilities/currencyFormat";

export default function OrderSummary({items}: { items: CartItem[] }) {
  const subtotal = items.reduce((acc, item) => acc + item.product.price * item.quantity, 0)
  const shipping = items.length > 0 ? 5 : 0
  const total = subtotal + shipping

  return (<div className="w-full p-8 bg-gray-50 rounded-xl">
    <h2 className="text-lg font-medium text-gray-900">Order summary</h2>
    <dl className="mt-8">
      <div className="flex justify-between items-center">
        <dt className="text-gray-600 text-sm">Subtotal</dt>
        <dd className="text-gray-900 text-sm font-medium">
          {currencyFormat(subtotal)}
        </dd>
      </div>
      <div className="flex justify-between items-center mt-5 pt-5 border-t border-gray-200">
        <dt className="text-gray-600 text-sm">Shipping estimate</dt>
        <dd className="text-gray-900 text-sm font-medium">
          {currencyFormat(shipping)}
        </dd>
      </div>
      <div className="flex justify-between items-center mt-5 pt-5 border-t border-gray-200">
        <dt className="text-gray-900 text-base font-medium">Order total</dt>
        <dd className="text-gray-900 text-base font-medium">
          {currencyFormat(total)}
        </dd>
      </div>
    </dl>
    <div className="mt-8">
      <Link to="/checkout"
            className="inline-flex items-center px-6 py-3 justify-center border border-transparent text-base font-medium rounded-md shadow-sm text-white bg-neutral-950 hover:bg-neutral-900 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-900 w-full transition-all">
        Checkout
      </Link>
    </div>
  </div>);
}
import {createContext, ReactNode, useContext, useEffect, useState} from "react";
import {CartItem} from "../models/CartItem";

type ShoppingCartProviderProps = {
  children: ReactNode
}

type ShoppingCart = {
  items: CartItem[],
  addItem: (item: CartItem) => Promise<void>,
  updateItem: (item: CartItem) => Promise<void>,
  removeItem: (item: CartItem) => Promise<void>,
  clear: () => Promise<void>,
}

const ShoppingCartContext = createContext({} as ShoppingCart)

export function useShoppingCart() {
  return useContext(ShoppingCartContext)
}


export function ShoppingCartProvider({children}: ShoppingCartProviderProps) {
  const [cartItems, setCartItems] = useState<CartItem[]>([])

  useEffect(() => {
    async function fetchBasket() {
      const response = await fetch(`${import.meta.env.VITE_API_BASE_URL}/api/basket`)
      const items = await response.json()
      setCartItems(items)
    }

    fetchBasket().catch((e) => console.error(e))
  }, [])

  async function addItem(item: CartItem) {
    const existingItem = cartItems.find((i) => i.product.productID === item.product.productID)
    if (existingItem) {
      existingItem.quantity += item.quantity
      await updateItem(existingItem)
    } else {
      await fetch(`${import.meta.env.VITE_API_BASE_URL}/api/basket`, {
        method: 'POST', headers: {
          'Content-Type': 'application/json'
        }, body: JSON.stringify(item)
      })
      setCartItems([...cartItems, item])
    }
  }

  async function updateItem(item: CartItem) {
    await fetch(`${import.meta.env.VITE_API_BASE_URL}/api/basket/${item.product.productID}`, {
      method: 'PUT', headers: {
        'Content-Type': 'application/json'
      }, body: JSON.stringify(item)
    })
    setCartItems([...cartItems.map((i) => i.product.productID === item.product.productID ? item : i)])
  }

  async function removeItem(item: CartItem) {
    await fetch(`${import.meta.env.VITE_API_BASE_URL}/api/basket/${item.product.productID}`, {
      method: 'DELETE'
    })
    console.log(item)
    console.log(cartItems)
    setCartItems([...cartItems.filter((i) => i.product.productID !== item.product.productID)])
  }

  async function clear() {
    await fetch(`${import.meta.env.VITE_API_BASE_URL}/api/basket`, {
      method: 'DELETE'
    })
    setCartItems([])
  }

  return <ShoppingCartContext.Provider value={{
    items: cartItems, addItem, updateItem, removeItem, clear,
  } as ShoppingCart}>
    {children}
  </ShoppingCartContext.Provider>
}
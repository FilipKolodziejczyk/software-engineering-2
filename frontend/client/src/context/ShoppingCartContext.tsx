import {createContext, ReactNode, useContext, useState} from "react";
import {CartItem} from "../models/CartItem";

type ShoppingCartProviderProps = {
  children: ReactNode
}

type ShoppingCart = {
  items: any[],
  addItem: (item: any) => void,
  updateItem: (item: any) => void,
  removeItem: (item: any) => void,
  clear: () => void,
}

const ShoppingCartContext = createContext({} as any)

export function useShoppingCart() {
  return useContext(ShoppingCartContext)
}


export function ShoppingCartProvider({children}: ShoppingCartProviderProps) {
  const [cartItems, setCartItems] = useState<CartItem[]>([])

  function addItem(item: CartItem) {
    const existingItem = cartItems.find((i) => i.product.productID === item.product.productID)
    if (existingItem) {
      existingItem.quantity += item.quantity
      updateItem(existingItem)
    }
    else {
      setCartItems([...cartItems, item])
    }
  }

  function updateItem(item: CartItem) {
    setCartItems([...cartItems.filter((i) => i.product.productID !== item.product.productID), item])
  }

  function removeItem(item: CartItem) {
    setCartItems([...cartItems.filter((i) => i.product.productID !== item.product.productID)])
  }

  function clear() {
    setCartItems([])
  }

  return <ShoppingCartContext.Provider value={{
    items: cartItems,
    addItem,
    updateItem,
    removeItem,
    clear
  } as ShoppingCart
  }>{children}</ShoppingCartContext.Provider>
}
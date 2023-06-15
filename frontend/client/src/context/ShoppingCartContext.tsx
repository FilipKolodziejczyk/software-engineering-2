import { createContext, ReactNode, useContext, useEffect, useState } from "react";
import { CartItem } from "../models/CartItem";
import axios from "axios";
import {debounce} from "@mui/material";

type ShoppingCartProviderProps = {
  children: ReactNode;
};

type ShoppingCart = {
  items: CartItem[];
  addItem: (item: CartItem) => Promise<void>;
  updateItem: (item: CartItem) => Promise<void>;
  removeItem: (item: CartItem) => Promise<void>;
  clear: () => Promise<void>;
};

const ShoppingCartContext = createContext({} as ShoppingCart);

export function useShoppingCart() {
  return useContext(ShoppingCartContext);
}
const updateItemDebounced = debounce(async (item: CartItem) => {
  try {
    await axios.put(
      `${import.meta.env.VITE_API_BASE_URL}/api/basket/${item.product.productID}`,
      item
    );
  } catch (error) {
    console.error(error);
  }
}, 500);


export function ShoppingCartProvider({ children }: ShoppingCartProviderProps) {
  const [cartItems, setCartItems] = useState<CartItem[]>([]);

  useEffect(() => {
    async function fetchBasket() {
      try {
        const response = await axios.get(`${import.meta.env.VITE_API_BASE_URL}/api/basket`);
        const items = response.data;
        setCartItems(items);
      } catch (error) {
        console.error(error);
      }
    }

    fetchBasket();
  }, []);

  async function addItem(item: CartItem) {
    const existingItem = cartItems.find((i) => i.product.productID === item.product.productID);
    if (existingItem) {
      existingItem.quantity += item.quantity;
      await updateItem(existingItem);
    } else {
      try {
        await axios.post(`${import.meta.env.VITE_API_BASE_URL}/api/basket`, {
          productID: item.product.productID,
          quantity: item.quantity,
        });
        setCartItems([...cartItems, item]);
      } catch (error) {
        console.error(error);
      }
    }
  }

  async function updateItem(item: CartItem) {
    try {
      await updateItemDebounced(item);
      setCartItems([...cartItems.map((i) => (i.product.productID === item.product.productID ? item : i))]);
    } catch (error) {
      console.error(error);
    }
  }

  async function removeItem(item: CartItem) {
    try {
      await axios.delete(`${import.meta.env.VITE_API_BASE_URL}/api/basket/${item.product.productID}`);
      setCartItems([...cartItems.filter((i) => i.product.productID !== item.product.productID)]);
    } catch (error) {
      console.error(error);
    }
  }

  async function clear() {
    try {
      await axios.delete(`${import.meta.env.VITE_API_BASE_URL}/api/basket`);
      setCartItems([]);
    } catch (error) {
      console.error(error);
    }
  }

  return (
    <ShoppingCartContext.Provider
      value={{
        items: cartItems,
        addItem,
        updateItem,
        removeItem,
        clear,
      }}
    >
      {children}
    </ShoppingCartContext.Provider>
  );
}

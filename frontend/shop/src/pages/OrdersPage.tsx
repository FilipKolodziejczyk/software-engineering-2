import { Link } from "react-router-dom";
import OrderItem from "../components/OrderItem";
import { useState, useContext, useEffect } from "react";
import { Order } from "../models/Order";
import { Context } from "../App";
import { properties } from "../resources/properties";

function OrdersPage  ()  {  
    const [orders, setOrders] = useState<Order[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(false);
    const [page, setPage] = useState(1);
    const [hasMore, setHasMore] = useState(false);
    const [searchQuery, setSearchQuery] = useState("");
    const { token, setToken } = useContext(Context);

    useEffect(() => { 
        setOrders([]);
        getOrders();
    }, [page]);

    const getOrders = async () => {
        setLoading(true);
        setError(false);
        await fetch(`${properties.url}/api/orders`, {
          method: "GET",
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-type": "application/json; charset=UTF-8",
          },
        })
          .then((response) => {
            if (response.ok) return response.json();
            else {
              throw new Error("ERROR " + response.status);
            }
          })
          .then((data) => {
            setOrders((prevOrders) => [...prevOrders, ...data]);
            setHasMore(data.length > 0);
            setLoading(false);
          })
          .catch((e) => {
            console.log("Error when trying to fetch orders: " + e);
          });
    };

    const updateList = () => {
        setOrders([]);
        getOrders();
    };
    
    return (
        <div className="flex flex-col items-center justify-center h-screen w-5/6 p-4 bg-gray-300">
            <div className="h-full overflow-y-auto w-full mt-10">
                {orders.map((order) => (<OrderItem key={order.orderID} order={order} updateList={updateList}/>))}
            </div>
        </div>
    )
};

export default OrdersPage;
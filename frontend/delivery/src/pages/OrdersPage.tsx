import { Link } from "react-router-dom";
import OrderItem from "../components/OrderItem";

function OrdersPage  ()  {  
  
  return (
    <div className="flex flex-col items-center justify-center h-screen w-5/6 p-4 bg-gray-300">
        <div className="h-full overflow-y-auto w-full">
            <OrderItem></OrderItem>
            <OrderItem></OrderItem>
            <OrderItem></OrderItem>
        </div>
    </div>
    )
};

export default OrdersPage;
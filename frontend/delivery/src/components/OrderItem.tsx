import { useState } from "react";
import { Order } from "../models/Order";

export interface OrderItemProps {
   // order: Order;
   // updateList: () => void;
}

const OrderItem: React.FC<OrderItemProps> = (props: OrderItemProps) => {

    return (
        <div className="m-2 bg-gray-100 rounded p-1">
            <div className="flex flex-col md:flex-row">
                <div className="w-full flex flex-col items-start justify-start justify-start md:w-4/5 p-4">
                    <div className="text-gray-950">order ID: </div>
                    <span className="text-gray-950 mt-2">street: </span>
                    <span className="text-gray-950 mt-2">city: </span>
                    <span className="text-gray-950 mt-2">postal code: </span>
                    <span className="text-gray-950 mt-2">country: </span>
                </div>
                <div className="w-full md:w-1/5 p-4 flex flex-col items-end justify-end justify-end">
                    <button className="w-1/2 bg-gray-300 hover:bg-gray-300 text-gray-950 py-2 px-4 rounded">
                        delivered
                    </button>
                </div>
            </div>
            
        </div>
    );

};

export default OrderItem;
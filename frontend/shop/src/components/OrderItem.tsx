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
                <div className="w-full md:w-2/5 p-4">
                    <div className="mt-2 text-lg font-medium text-gray-950">Flower name</div>
                </div>
                <div className="w-full md:w-2/5 p-4">
                    <div className="flex flex-col">
                        <span className="text-gray-950">Price: </span>
                        <span className="text-gray-950 mt-2">XXX: </span>
                        <span className="text-gray-950 mt-2">XXX: </span>
                    </div>
                </div>
                <div className="w-full md:w-1/5 p-4 flex flex-col items-end justify-end justify-end">
                    <button className="w-1/2 bg-gray-300 hover:bg-gray-300 text-gray-950 py-2 px-4 rounded">
                        accept
                    </button>
                    <button className="w-1/2 bg-gray-300 hover:bg-gray-300 text-gray-950 py-2 px-4 rounded mt-1">
                        decline
                    </button>
                    <button className="w-1/2 bg-gray-300 hover:bg-gray-300 text-gray-950 py-2 px-4 rounded mt-1">
                        send message
                    </button>
                </div>
            </div>
            <div className="flex flex-col md:flex-row">
                <div className="w-full p-4">
                    <input className="bg-gray-300 w-full text-gray-950 ml-2 rounded" type="text" placeholder="message to customer" />
                </div>
            </div>
        </div>
    );

};

export default OrderItem;
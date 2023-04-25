import { useState } from "react";
import { Complaint } from "../models/Complaint";

export interface ComplaintItemProps {
   // complaint: Complaint;
   // updateList: () => void;
}

const OrderItem: React.FC<ComplaintItemProps> = (props: ComplaintItemProps) => {

    return (
        <div className="m-2 bg-gray-100 rounded p-1">
            <div className="flex flex-col md:flex-row">
                <div className="w-full md:w-1/5 p-4">
                    <div className="mt-2 text-lg font-medium text-gray-950">order id</div>
                </div>
                <div className="w-full md:w-3/5 p-4">
                    <div className="flex flex-col">
                        <span className="text-gray-950">complaint message </span>
                    </div>
                </div>
                <div className="w-full md:w-1/5 p-4 flex flex-col items-end justify-end justify-end">
                    <button className="w-1/2 bg-gray-300 hover:bg-gray-300 text-gray-950 py-2 px-4 rounded">
                        send reply
                    </button>
                    <button className="w-1/2 bg-gray-300 hover:bg-gray-300 text-gray-950 py-2 px-4 rounded mt-1">
                        reject
                    </button>
                </div>
            </div>
            <div className="flex flex-col md:flex-row">
                <div className="w-full p-4">
                    <input className="bg-gray-300 w-full text-gray-950 ml-2 rounded" type="text" placeholder="reply to complaint" />
                </div>
            </div>
        </div>
    );

};

export default OrderItem;
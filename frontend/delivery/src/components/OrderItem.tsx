import { useContext, useState } from "react";
import { Order } from "../models/Order";
import { properties } from "../resources/properties";
import { Context } from "../App";

export interface OrderItemProps {
   order: Order;
   updateList: () => void;
}

const OrderItem: React.FC<OrderItemProps> = (props: OrderItemProps) => {
    const { token, setToken } = useContext(Context);

    const deliveredHandle = async () => {
            await fetch(`${properties.url}/api/orders/${props.order.orderID}/change_status`, {
                method: "PUT",
                headers: {
                  Authorization: `Bearer ${token}`,
                  "Content-type": "application/json; charset=UTF-8",
                },
                body: JSON.stringify({"orderStatus": "delivered"})
              }).then((response) => {
                  if (response.ok) return response.json()
                  else throw new Error("ERROR " + response.status)
                }).then(() => {
                  console.log("Success accepting order.")
                }).catch((e) => {
                  console.log("Error when trying to accept order: " + e)
                }).finally(()=>{
                  props.updateList();
                });
          }

    return (
        <div className="m-2 bg-gray-100 rounded p-1">
            <div className="flex flex-col md:flex-row">
                <div className="w-full flex flex-col items-start justify-start justify-start md:w-4/5 p-4">
                    <span className="text-gray-950">Order ID: {props.order.orderID}</span>
                    <span className="text-gray-950 mt-2">Client ID: {props.order.clientID}</span>
                    <span className="text-gray-950 mt-2">Status: {props.order.status}</span>
                    <span className="text-gray-950 mt-2">{props.order.address.street} {props.order.address.buildingNo}/{props.order.address.houseNo}</span>
                    <span className="text-gray-950 mt-2">{props.order.address.postalCode} {props.order.address.city}</span>
                    <span className="text-gray-950 mt-2">{props.order.address.country}</span>
                </div>
                <div className="w-full md:w-1/5 p-4 flex flex-col items-end justify-end justify-end">
                    <button className="w-1/2 bg-gray-300 hover:bg-gray-300 text-gray-950 py-2 px-4 rounded" onClick={()=>deliveredHandle()}>
                        delivered
                    </button>
                </div>
            </div>
        </div>
    );

};

export default OrderItem;
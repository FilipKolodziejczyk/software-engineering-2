import { Link } from "react-router-dom";
import { ProductNew } from "../models/ProductNew";
import { useContext, useState } from "react";
import { properties } from "../resources/properties";
import { Context } from "../App";

function ProductItemAddPage () {

  let product_: ProductNew = {
    name: "",
    price: -1,
    quantity: -1,
    description: "",
    image: "",
    category: "none",
  };

    const [addedProduct, setAddedProduct] = useState(product_);
    const { token, setToken } = useContext(Context);

    const saveHandle = async () => {
      await fetch(`${properties.url}/api/products`, {
        method: "POST",
        headers: {
            'Authorization': `Bearer ${token}`,
            "Content-type": "application/json",
        },
        body: JSON.stringify(addedProduct)

      })
        .then((response) => {
          if (response.ok) return response.json();
          else {
            throw new Error("ERROR " + response.status);
          }
        })
        .then(() => {
          console.log("Success adding product.");
        })
        .catch((e) => {
          console.log("Error when trying to add product: " + e);
          console.log(product_);
        })
    }

    return (
    <div className="flex flex-col items-center justify-center h-screen w-5/6 p-4 bg-gray-300">
    <div className="flex flex-row m-2 bg-gray-100 rounded p-1">
      <div className="w-1/3 flex flex-col">
        <div className="w-full">
          <img
            className="w-full object-cover rounded"
            src="https://img.freepik.com/free-photo/purple-osteospermum-daisy-flower_1373-16.jpg?w=2000"
            alt="example image"
          />
        </div>
        <input className="bg-gray-100 text-gray-950 ml-2 mt-2 rounded" type="text" placeholder="product name" onChange={(e)=>setAddedProduct({...addedProduct, name: e.target.value})}/>
      </div>
      <div className="w-1/3">
        <div className="flex flex-col p-1">
          <input className="bg-gray-100 text-gray-950 ml-2 rounded" type="text" placeholder="price" onChange={(e)=>setAddedProduct({...addedProduct, price: Number(e.target.value)})}/>
          <input className="bg-gray-100 text-gray-950 ml-2 mt-2 rounded" type="text" placeholder="quantity" onChange={(e)=>setAddedProduct({...addedProduct, quantity: Number(e.target.value)})}/>
          <input className="bg-gray-100 text-gray-950 ml-2 mt-2 rounded" type="text" placeholder="category" onChange={(e)=>setAddedProduct({...addedProduct, category: e.target.value})} />
          <input className="bg-gray-100 text-gray-950 ml-2 mt-2 rounded" type="text" placeholder="description" onChange={(e)=>setAddedProduct({...addedProduct, description: e.target.value})} />

        </div>
      </div>
      <div className="w-1/3 flex flex-col items-end justify-end justify-end">
        <Link to="/shop/products">
        <button className="bg-gray-300 hover:bg-gray-300 text-gray-950 py-2 px-4 rounded" onClick={()=>saveHandle()}>
          save
        </button>
        </Link>
        <Link to="/shop/products">
        <button className="bg-gray-300 hover:bg-gray-300 text-gray-950 py-2 px-4 rounded mt-1">
          cancel
        </button>
        </Link>
      </div>
    </div>
    </div>
    );

};

export default ProductItemAddPage;
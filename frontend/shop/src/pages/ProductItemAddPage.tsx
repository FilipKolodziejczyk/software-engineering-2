import { Link } from "react-router-dom";
import { Product } from "../models/Product";
import { useState } from "react";

function ProductItemAddPage () {

  let product_: Product = {
    productID: -1,
    name: "",
    price: -1,
    quantity: -1,
    description: "",
    image: "",
    archived: false,
    category: "",
  };

    const [addedProduct, setAddedProduct] = useState(product_);

    const saveHandle = () => {
      //fetch(`https://fakestoreapi.com/api/products/${props.product.productID}`).then(res => res.json()).then((data) => {
       // method: "POST",
       // body: JSON.stringify(currentProduct)
     // }).catch(() => {
        
     // });
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
        <Link to="/products">
        <button className="bg-gray-300 hover:bg-gray-300 text-gray-950 py-2 px-4 rounded" onClick={()=>saveHandle()}>
          save
        </button>
        </Link>
        <Link to="/products">
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
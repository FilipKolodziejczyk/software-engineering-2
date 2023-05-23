import { useState } from "react";
import { Product } from "../models/Product";

export interface ProductItemProps {
   product: Product;
   updateList: () => void;
}

const ProductItem: React.FC<ProductItemProps> = (props: ProductItemProps) => {

    const [editing, setEditing] = useState(false);
    const [currentProduct, setCurrentProduct] = useState(props.product);
    
    const discardlHandle = () => {
      setEditing(false);
    }

    const saveHandle = () => {
      //fetch(`https://fakestoreapi.com/api/products/${props.product.productID}`).then(res => res.json()).then((data) => {
       // method: "PUT",
       // body: JSON.stringify(currentProduct)
     // }).catch(() => {
        
     // });
        props.updateList();
        setEditing(false);
    }

    const deleteHandle = () => {
      //fetch(`https://fakestoreapi.com/api/products/${props.product.productID}`).then(res => res.json()).then((data) => {
       // method: "DELETE",
       // body: JSON.stringify(currentProduct)
     // }).catch(() => {
     // });
        props.updateList();
        setEditing(false);
    }

    return (
      <div>
      {editing ? 
      <div className="flex flex-row m-2 bg-gray-100 rounded p-1">
      <div className="w-1/3 flex flex-col">
        <div className="w-full">
          <img
            className="w-full object-cover rounded" // add image
            src="https://img.freepik.com/free-photo/purple-osteospermum-daisy-flower_1373-16.jpg?w=2000"
            alt="example image"
          />
        </div>
        <input className="bg-gray-100 mt-2 text-lg font-medium text-gray-950" defaultValue={String(props.product.name)} onChange={(e)=>setCurrentProduct({...currentProduct, name: e.target.value})}></input>

      </div>
      <div className="w-1/3">
        <div className="flex flex-col">
          <input className="bg-gray-100 text-gray-950 ml-2 mt-2 rounded" defaultValue={String(props.product.price)} onChange={(e)=>setCurrentProduct({...currentProduct, price: Number(e.target.value)})}></input>
          <input className="bg-gray-100 text-gray-950 ml-2 mt-2 rounded" defaultValue={String(props.product.quantity)} onChange={(e)=>setCurrentProduct({...currentProduct, quantity: Number(e.target.value)})}></input>
          <input className="bg-gray-100 text-gray-950 ml-2 mt-2 rounded" defaultValue={String(props.product.category)} onChange={(e)=>setCurrentProduct({...currentProduct, category: e.target.value})}></input>
          <input className="bg-gray-100 text-gray-950 ml-2 mt-2 rounded" defaultValue={String(props.product.description)} onChange={(e)=>setCurrentProduct({...currentProduct, description: e.target.value})}></input>
          <input className="bg-gray-100 text-gray-950 ml-2 mt-2 rounded" defaultValue={String(props.product.archived)} onChange={(e)=>setCurrentProduct({...currentProduct, archived: Boolean(e.target.value)})}></input>
        </div>
      </div>
      <div className="w-1/3 flex flex-col items-end justify-end justify-end">
        <button className="w-1/2 bg-gray-300 hover:bg-gray-300 text-gray-950 py-2 px-4 rounded" onClick={()=>saveHandle()}>
          save
        </button>
        <button className="w-1/2 bg-gray-300 hover:bg-gray-300 text-gray-950 py-2 px-4 rounded mt-1" onClick={()=>{discardlHandle(); setCurrentProduct(props.product);}}>
          discard
        </button>
      </div>
    </div>
  :
    <div className="flex flex-row m-2 bg-gray-100 rounded p-1">
      <div className="w-1/3 flex flex-col">
        <div className="w-full">
          <img
            className="w-full object-cover rounded" // add image
            src="https://img.freepik.com/free-photo/purple-osteospermum-daisy-flower_1373-16.jpg?w=2000"
            alt="example image"
          />
        </div>
        <div className="mt-2 text-lg font-medium text-gray-950">Name {props.product.name}</div>
      </div>
      <div className="w-1/3">
        <div className="flex flex-col">
          <span className="text-gray-950">Price: {String(props.product.price)}</span>
          <span className="text-gray-950 mt-2">Quantity:  {String(props.product.quantity)}</span>
          <span className="text-gray-950 mt-2">Category: {props.product.category}</span>
          <span className="text-gray-950 mt-2">Description: {props.product.description}</span>
          <span className="text-gray-950 mt-2">Archived: {String(props.product.archived)}</span>
        </div>
      </div>
      <div className="w-1/3 flex flex-col items-end justify-end justify-end">
        <button className="w-1/2 bg-gray-300 hover:bg-gray-300 text-gray-950 py-2 px-4 rounded" onClick={()=>setEditing(!editing)}>
          edit
        </button>
        <button className="w-1/2 bg-gray-300 hover:bg-gray-300 text-gray-950 py-2 px-4 rounded mt-1" onClick={()=>deleteHandle()}>
          delete
        </button>
      </div>
    </div>
    }
    </div>
    );

};

export default ProductItem;
import { useState } from "react";
import { Product } from "../models/Product";

export interface ProductItemProps {
   // product: Product;
   // updateList: () => void;
}

const ProductItem: React.FC<ProductItemProps> = (props: ProductItemProps) => {

    return (
    <div className="flex flex-row m-2 bg-gray-100 rounded p-1">
      <div className="w-1/3 flex flex-col">
        <div className="w-full">
          <img
            className="w-full object-cover rounded"
            src="https://img.freepik.com/free-photo/purple-osteospermum-daisy-flower_1373-16.jpg?w=2000"
            alt="example image"
          />
        </div>
        <div className="mt-2 text-lg font-medium text-gray-950">Flower name</div>
      </div>
      <div className="w-1/3">
        <div className="flex flex-col">
          <span className="text-gray-950">Price: </span>
          <span className="text-gray-950 mt-2">XXX: </span>
          <span className="text-gray-950 mt-2">XXX: </span>
        </div>
      </div>
      <div className="w-1/3 flex flex-col items-end justify-end justify-end">
        <button className="w-1/2 bg-gray-300 hover:bg-gray-300 text-gray-950 py-2 px-4 rounded">
          edit
        </button>
        <button className="w-1/2 bg-gray-300 hover:bg-gray-300 text-gray-950 py-2 px-4 rounded mt-1">
          delete
        </button>
      </div>
    </div>
    );

};

export default ProductItem;
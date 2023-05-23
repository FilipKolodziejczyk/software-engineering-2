import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import ProductItem from "../components/ProductItem";
import ProductItemAdd from "./ProductItemAddPage";
import { Product } from "../models/Product";

function ProductsPage  ()  {  

    const [products, setProducts] = useState<Product[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(false);
    const [page, setPage] = useState(1);
    const [hasMore, setHasMore] = useState(false);
    const [searchQuery, setSearchQuery] = useState("");

    useEffect(() => { // add search in fetch
        setLoading(true);
        setError(false);
        fetch(`https://fakestoreapi.com/products?limit=10&page=${page}`).then(res => res.json()).then((data) => {
          setProducts((prevProducts) => [...prevProducts, ...data]);
          setHasMore(data.length > 0);
          setLoading(false);
        }).catch(() => {
          setError(true);
        });
    }, [page, searchQuery]);

    const getProducts = () => {
        setLoading(true);
        setError(false);
        fetch(`https://fakestoreapi.com/products?limit=10&page=${page}`).then(res => res.json()).then((data) => {
          setProducts((prevProducts) => [...prevProducts, ...data]);
          setHasMore(data.length > 0);
          setLoading(false);
        }).catch(() => {
          setError(true);
        });
    };

    const updateList = () => {
        getProducts();
    };

    return (
    
    <div className="flex flex-col items-center justify-center h-screen w-5/6 p-4 bg-gray-300">
        <div className="flex justify-end absolute top-2 right-4">
            <Link to="/products/new">
            <button className="bg-gray-100 text-gray-950 py-2 px-4 rounded hover:bg-gray-300 font-medium py-2 px-4">
                add
            </button>
            </Link>
            <input className="bg-gray-100 text-gray-950 ml-2 rounded" type="text" placeholder="search" onChange={(e)=>setSearchQuery(e.target.value)}/>
        </div>

        <div className="h-full overflow-y-auto w-full mt-10">
            {products.map((product) => (<ProductItem key={product.productID} product={product} updateList={updateList}/>))}
        </div>
    </div>
    )
};

export default ProductsPage;
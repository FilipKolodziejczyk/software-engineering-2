import {useEffect, useState} from "react";
import ProductCard from "../components/ProductCard";
import {Product} from "../models/Product";
import {ArrowPathIcon} from "@heroicons/react/20/solid";

const sortOptions = [{value: "featured", label: "Featured"}, {
  value: "price-asc", label: "Price ascending"
}, {value: "price-desc", label: "Price descending"}, {value: "title-asc", label: "Name"},];
export default function ProductsPage() {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(false);
  const [page, setPage] = useState(1);
  const [hasMore, setHasMore] = useState(false);
  const [sort, setSort] = useState(sortOptions[0].value);

  useEffect(() => {
    setLoading(true);
    setError(false);
    fetch(`${import.meta.env.VITE_API_BASE_URL}/api/products?page=${page}&limit=10`).then(res => res.json()).then((data) => {
      setProducts((prevProducts) => [...prevProducts, ...data]);
      setHasMore(data.length > 0);
      setLoading(false);
    }).catch(() => {
      setError(true);
    });
  }, [page]);

  const onScroll = () => {
    if (window.innerHeight + document.documentElement.scrollTop !== document.documentElement.offsetHeight || loading) return;
    if (!hasMore) return;
    setPage((prevPage) => prevPage + 1);
  }

  useEffect(() => {
    window.addEventListener("scroll", onScroll);
    return () => window.removeEventListener("scroll", onScroll);
  }, [products]);

  return (<div className="mx-auto w-full max-w-2xl px-4 py-4 sm:px-6 sm:py-8 lg:max-w-5xl 2xl:max-w-7xl lg:px-8">
    <h1 className="text-2xl font-bold sm:text-3xl sm:font-extrabold tracking-tight text-gray-900">Products</h1>
    <div className="flex justify-between items-center">
      {/*<p className="text-sm text-gray-700">Showing {products.length} results</p>*/}
      <div className="flex items-center gap-2">
        <label htmlFor="sort" className="text-sm text-gray-700">Sort by</label>
        <select id="sort" name="sort"
                className=" rounded-md bg-white py-1.5 pl-3 pr-10 text-left text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 focus:outline-none focus:ring-2 focus:ring-indigo-500 sm:text-sm sm:leading-6"
                value={sort} onChange={(e) => setSort(e.target.value)}>
          {sortOptions.map((option) => (
            <option key={option.value} value={option.value}>{option.label}</option>
          ))}
        </select>
      </div>
    </div>
    <div className="mt-6 grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-x-6 gap-y-10">
      {products.map((product) => (<ProductCard key={product.productID} product={product}/>))}
    </div>
    <div className="flex justify-center mt-2">
      {loading && <p className="flex items-center gap-2">Loading <ArrowPathIcon className="w-4 h-4 animate-spin"/></p>}
      {error && <p>Error</p>}
    </div>
  </div>);
}
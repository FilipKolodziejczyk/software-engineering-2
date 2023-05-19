import {Fragment, useEffect, useState} from "react";
import ProductCard, {ProductCardSkeleton} from "../components/ProductCard";
import {Product} from "../models/Product";
import {ArrowPathIcon} from "@heroicons/react/20/solid";
import {CategoryFilters} from "../components/CategoryFilters";

export default function ProductsPage() {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(false);
  const [page, setPage] = useState(1);
  const [hasMore, setHasMore] = useState(false);
  const [isMounted, setIsMounted] = useState(false);

  useEffect(() => {
    setIsMounted(true);
    return () => setIsMounted(false);
  }, []);

  useEffect(() => {
    if (!isMounted) return;
    setLoading(true);
    setError(false);
    fetch(`${import.meta.env.VITE_API_BASE_URL}/api/products?page=${page}&limit=10`).then(res => res.json()).then((data) => {
      setProducts((prevProducts) => [...prevProducts, ...data]);
      setHasMore(data.length > 0);
      setLoading(false);
    }).catch(() => {
      setError(true);
    });
  }, [page, isMounted]);

  const onScroll = () => {
    if (window.innerHeight + document.documentElement.scrollTop !== document.documentElement.offsetHeight || loading) return;
    if (!hasMore) return;
    setPage((prevPage) => prevPage + 1);
  }

  useEffect(() => {
    window.addEventListener("scroll", onScroll);
    return () => window.removeEventListener("scroll", onScroll);
  }, [products]);

  return (<CategoryFilters>
    <div className="mx-auto w-full max-w-2xl px-4 py-4 sm:px-6 sm:py-8 sm:max-w-5xl 2xl:max-w-7xl lg:px-8">
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-x-6 gap-y-10">
        {
          products.length > 0 ? <Fragment>{products.map((product) => (<ProductCard key={product.productID} product={product}/>))}</Fragment>
            : <Fragment>
              {Array.from(Array(10).keys()).map((i) => (<ProductCardSkeleton key={i}/>))}
            </Fragment>
        }

      </div>
      <div className="flex justify-center mt-2">
        {loading &&
            <p className="flex items-center gap-2">Loading <ArrowPathIcon className="w-4 h-4 animate-spin"/></p>}
        {error && <p>Error</p>}
      </div>
    </div>
  </CategoryFilters>);
}
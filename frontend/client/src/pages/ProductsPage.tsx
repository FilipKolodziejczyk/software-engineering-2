import {Fragment, useEffect, useMemo, useState} from "react";
import ProductCard, {ProductCardSkeleton} from "../components/ProductCard";
import {Product} from "../models/Product";
import {CategoryFilters} from "../components/CategoryFilters";
import {useLocation} from "react-router-dom";
import SpinnerIcon from "../resources/SpinnerIcon";


export function loader({request}: { request: Request }) {
  let url = new URL(request.url);
  console.log(url)
  return null;
}

function useQuery() {
  const {search} = useLocation();

  return useMemo(() => new URLSearchParams(search), [search]);
}

export default function ProductsPage() {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(false);
  const [page, setPage] = useState(1);
  const [hasMore, setHasMore] = useState(false);
  const [isMounted, setIsMounted] = useState(false);

  let query = useQuery();

  useEffect(() => {
    setIsMounted(true);
    return () => setIsMounted(false);
  }, []);

  useEffect(() => {
    setProducts([]);
    setPage(1);
  }, [query]);

  useEffect(() => {
    if (!isMounted) return;
    setLoading(true);
    setError(false);

    let base_url = import.meta.env.VITE_API_BASE_URL;
    if (base_url === undefined) {
      console.error("API Base URL is undefined");
      base_url = window.location.origin;
    }
    console.log(`API Base URL: ${base_url}`);
    const url = new URL("/api/products", base_url);
    console.log(`URL: ${url}`)
    url.searchParams.append('pageNumber', page.toString());
    url.searchParams.append('elementsOnPage', '10');

    for (let [key, value] of query.entries()) {
      url.searchParams.append(key, value);
    }

    fetch(url).then(res => res.json()).then((data) => {
      setProducts((prevProducts) => [...prevProducts, ...data]);
      setHasMore(data.length > 0);
      setLoading(false);
    }).catch((reason) => {
      console.log(`Error while fetching products from ${url}`);
      console.error(reason);
      setError(true);
      setLoading(false);
    });
  }, [page, isMounted, query]);

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
          products.length <= 0 && !loading
            ? <div className="col-span-3 w-full text-center">
              <p className="text-zinc-600 text-sm">No products found.</p>
            </div>
            : products.length <= 0 && loading
              ? <Fragment>
                {Array.from(Array(10).keys()).map((i) => (<ProductCardSkeleton key={i}/>))}
              </Fragment>
              : <Fragment>{products.map((product, index) => (<ProductCard key={index} product={product}/>))}</Fragment>
        }

      </div>
      <div className="flex justify-center items-center mt-24">
        {loading &&
            <div className="flex items-center gap-2 ">
                <p className="text-zinc-600 text-sm">Loading</p>
                <SpinnerIcon/>
            </div>
        }
        {error && <p>An error occurred while loading products.</p>}
      </div>
    </div>
  </CategoryFilters>);
}
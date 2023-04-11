import {Link, useRouteError} from "react-router-dom";

export default function ErrorPage() {
  const error = useRouteError() as { statusText?: string; status?: number };
  console.log(error);

  return (
    <div className="flex flex-col place-items-center justify-center h-screen w-full">
      <p className="text-9xl font-thin">{error.status}</p>
      <p className="text-xl font-light">{error.statusText}</p>
      <Link to="/">
        <div
          className="mt-8 px-4 py-2 border-gray-500 border rounded-full hover:bg-gray-50 transition-color font-light">Back
          to the
          homepage
        </div>
      </Link>
    </div>
  );
}
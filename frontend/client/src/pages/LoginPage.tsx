import { Link, useNavigate } from "react-router-dom";
import React, { useState, useEffect } from "react";
import { ArrowUturnLeftIcon } from "@heroicons/react/20/solid";
import { useAuth } from "../context/AuthContext";

export default function LoginPage() {
  const { token, setToken } = useAuth();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState<Error | null>(null); // [1
  const navigate = useNavigate();

  console.log(token)

  const handleLogin = () => {
    // Send user credentials to /api/users/log_in
    fetch(`${import.meta.env.VITE_API_BASE_URL}/api/users/log_in`, {
      method: "POST",
      body: JSON.stringify({ username, password }),
      headers: {
        "Content-Type": "application/json"
      }
    })
      .then(response => {
        if (response.ok) {
          return response.json();
        } else if (response.status === 401) {
          setError(new Error("Invalid username or password"));
        } else {
          setError(new Error("An unknown error occurred"));
        }
      })
      .then(data => {
        // Set the JWT token
        setToken(data.jwttoken);
        navigate("/");
      })
      .catch(error => {
        // Show error prompt
        console.error(error);
      });
  };

  const handleKeyPress = (event: React.KeyboardEvent<HTMLInputElement>) => {
    if (event.key === "Enter") {
      handleLogin();
    }
  };

  useEffect(() => {
    // Redirect the user when a token is set
    if (token) {
      navigate("/");
    }
  }, [token]);

  return (
    <div className="flex flex-col place-items-center py-16 w-full pt-12 sm:bg-indigo-50 h-screen">
      <div className="sm:border-gray-300 sm:rounded-xl bg-white sm:shadow-lg my-auto lg:grid lg:grid-cols-2">
        <div className="sm:p-12 lg:p-16 flex flex-col">
          <h1 className="text-2xl sm:text-4xl font-bold tracking-tight text-gray-900 w-full text-center">Welcome back!</h1>
          <div className="flex flex-col gap-2 mt-12 ">
            <label htmlFor="email">Email</label>
            <input
              type="email"
              id="email"
              name="email"
              placeholder="Email"
              className="border-gray-300 border rounded-md px-4 py-2 mb-2"
              value={username}
              onChange={e => setUsername(e.target.value)}
              onKeyDown={handleKeyPress}
            />

            <label htmlFor="password">Password</label>
            <input
              type="password"
              id="password"
              name="password"
              placeholder="Password"
              className="border-gray-300 border rounded-md px-4 py-2 mb-8"
              value={password}
              onChange={e => setPassword(e.target.value)}
              onKeyDown={handleKeyPress}
            />

            <div className="flex items-center justify-between">
              <div className="flex items-center mr-16">
                <input
                  type="checkbox"
                  id="remember"
                  name="remember"
                  className="border-gray-300 border rounded transition-colors cursor-pointer text-neutral-950 focus:ring-0 focus:ring-offset-0 form-checkbox"
                />
                <label
                  htmlFor="remember"
                  className="ml-2 text-sm font-light cursor-pointer select-none"
                >
                  Remember me
                </label>
              </div>
              <Link to="/forgot-password">
                <p className="text-sm font-light text-blue-500">Forgot password?</p>
              </Link>
            </div>
          </div>
          <div className="my-auto">

            {error && (
              <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative text-center" role="alert">
                <span className="block sm:inline">{error.message}</span>
              </div>
            )}

              <button className="button-main transition-colors w-full mt-4" onClick={handleLogin}>
                <p>Login</p>
              </button>

            <div className="flex flex-row justify-center gap-4 mt-8">
              <p className="text-sm font-light">Don't have an account?</p>
              <Link to="/register">
                <p className="text-sm font-light text-blue-500">Register</p>
              </Link>
            </div>
          </div>
        </div>
        <div className="hidden lg:block max-w-md max-h-full">
          <img
            src="https://images.unsplash.com/photo-1613375772563-af532af5cef9"
            className="object-cover rounded-r-xl"
           alt="Login"/>
        </div>
      </div>
      <div className="hidden sm:inline absolute top-4 left-4 xl:top-8 xl:left-8 max-w-sm max-h-sm bg-white p-2 shadow-sm text-sm rounded-xl">
        <Link
          to="/"
          className="text-gray-700 hover:text-gray-800 flex items-center transition-colors"
        >
          <ArrowUturnLeftIcon className="w-4 h-4 mr-2" /> Back to the store
        </Link>
      </div>
    </div>
  );
}
import {Link} from "react-router-dom";
import React from "react";

export default function LoginPage() {

  return (
    <div className="flex flex-col place-items-center py-16 w-full pt-12">
      <div className="sm:p-12 sm:border-gray-300 sm:border sm:rounded-xl">
        <h1 className="text-5xl font-thin">Welcome back!</h1>
        <div className="flex flex-col gap-2 mt-12 ">
          <label htmlFor="email">Email</label>
          <input type="email" id="email" name="email" placeholder="Email"
                 className="border-gray-300 border rounded-md px-4 py-2 mb-2"
          />

          <label htmlFor="password">Password</label>
          <input type="password" id="password" name="password" placeholder="Password"
                 className="border-gray-300 border rounded-md px-4 py-2 mb-8"
          />

          <div className="flex items-center justify-between">
            <div className="flex items-center mr-16">
              <input type="checkbox" id="remember" name="remember"/>
              <label htmlFor="remember" className="ml-2 text-sm font-light">Remember me</label>
            </div>
            <Link to="/forgot-password">
              <p className="text-sm font-light text-blue-500">Forgot password?</p>
            </Link>
          </div>

          <Link to="/">
            <div
              className="flex items-center justify-center gap-2 px-4 py-2 mt-8 rounded-full bg-neutral-950 hover:bg-neutral-900 text-white transition-color font-light">
              <p>Login</p>
            </div>
          </Link>

          <div className="flex flex-row justify-center gap-4 mt-8">
            <p className="text-sm font-light">Don't have an account?</p>
            <Link to="/register">
              <p className="text-sm font-light text-blue-500">Register</p>
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
}
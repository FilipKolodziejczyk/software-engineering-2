import React, {Fragment} from 'react';
import {Popover, Transition} from '@headlessui/react'
import {UserIcon} from "@heroicons/react/24/outline";
import {useAuth} from "../context/AuthContext";
import {Link} from "react-router-dom";

export default function UserPopover() {
  const {user} = useAuth()

  return (
    <Popover className="relative h-6">
      {({open}) => (<>
        <Popover.Button
          className={`
                ${open ? '' : 'text-opacity-90'}
                group inline-flex items-center rounded-md text-base font-medium hover:text-opacity-100 focus:outline-none focus-visible:ring-2 focus-visible:ring-white focus-visible:ring-opacity-75`}
        >
          <UserIcon
            className={`${open ? '' : 'text-opacity-70'}
                  h-6 w-6 transition duration-150 ease-in-out group-hover:text-opacity-80`}
            aria-hidden="true"
          />
        </Popover.Button>

        <Popover.Overlay className="fixed inset-0 bg-black opacity-30"/>
        <Transition
          as={Fragment}
          enter="transition ease-out duration-200"
          enterFrom="opacity-0 translate-y-1"
          enterTo="opacity-100 translate-y-0"
          leave="transition ease-in duration-150"
          leaveFrom="opacity-100 translate-y-0"
          leaveTo="opacity-0 translate-y-1"
        >
          <Popover.Panel
            className="absolute -right-40 z-10 mt-3 w-screen max-w-sm -translate-x-1/2 transform px-4 sm:px-0 lg:max-w-xs">
            <div className="overflow-hidden rounded-lg shadow-lg ring-1 ring-black ring-opacity-5 bg-white p-4">
              {user ? (<div className="flex flex-col">
                <span>Welcome, {user.name}</span>
                <Link to="/logout">
                  <span
                    className="block mt-4 px-4 py-1 text-sm text-gray-700 hover:bg-gray-100 hover:text-gray-900 rounded-md text-center text-sm">
                    Logout
                  </span>
                </Link>
              </div>) : (<>
                <Link to="/login">
                  <span
                    className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 hover:text-gray-900 rounded-md text-center">
                    Login
                  </span>
                </Link>
              </>)
              }
            </div>
          </Popover.Panel>
        </Transition>
      </>)}
    </Popover>
  );
}
import {MinusIcon, PlusIcon} from "@heroicons/react/20/solid";
import React from "react";

export function CounterInput(props: { onDecrease: () => void, value: number, onChange: (e: any) => void, onIncrease: () => void }) {
  return <>
    <div className="flex flex-row items-center justify-center w-12 h-12 rounded-md bg-gray-100 hover:bg-gray-200">
      <button type="button" className="w-6 h-6 text-gray-500 hover:text-gray-600"
              onClick={props.onDecrease}>
        <MinusIcon className="w-full h-full"/>
      </button>
    </div>
    <div className="flex flex-row items-center justify-center w-12 h-12 rounded-md bg-gray-100 mx-2">
      <input type="text" value={props.value} className="w-full h-full text-center text-gray-700"
             onChange={props.onChange}
      />
    </div>
    <div className="flex flex-row items-center justify-center w-12 h-12 rounded-md bg-gray-100 hover:bg-gray-200">
      <button type="button" className="w-6 h-6 text-gray-500 hover:text-gray-600"
              onClick={props.onIncrease}>
        <PlusIcon className="w-full h-full"/>
      </button>
    </div>
  </>;
}
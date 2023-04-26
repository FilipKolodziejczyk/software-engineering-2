import React, {useState} from "react";
import { Tab } from '@headlessui/react'
export function ImageCarousel(props: { images: string[] }) {
  const [selectedIndex, setSelectedIndex] = useState(0)

  return <div className="flex">
    <div className="mx-auto w-full">
      <Tab.Group selectedIndex={selectedIndex} onChange={setSelectedIndex}>
        <Tab.Panels>
          {props.images.map((image, index) => (
            <Tab.Panel key={index}>
              <img src={image} alt="Product Image" className="w-full aspect-square object-cover rounded-xl"/>
            </Tab.Panel>
          ))}
        </Tab.Panels>
        <Tab.List className="flex gap-6 mt-6">
          {props.images.map((image, index) => (
            <Tab key={index} className="w-1/3 rounded-md overflow-hidden border border-gray-200">
              <img src={image} alt="Product Image" className="w-full aspect-square object-cover"/>
            </Tab>
          ))}
        </Tab.List>
      </Tab.Group>
    </div>
  </div>;
}
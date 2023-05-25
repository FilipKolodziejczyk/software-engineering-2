import React, {useEffect, useState} from "react";
import {Tab} from '@headlessui/react'

export default function ImageCarousel(props: { imageSources: string[] }) {
  const [selectedIndex, setSelectedIndex] = useState(0)
  const [loading, setLoading] = useState(Array(props.imageSources.length).fill(true));

  useEffect(() => {
    const handleImageLoad = (index: number) => {
      setLoading(loading => {
        const newLoading = [...loading]
        newLoading[index] = false
        return newLoading
      })
    }

    const images = props.imageSources.map((image, index) => {
      const img = new Image();
      img.src = image;

      img.addEventListener('load', () => handleImageLoad(index));

      return img;
    });

    return () => {
      images.forEach((image) => {
        image.removeEventListener('load', () => handleImageLoad);
      });
    }
  }, [props.imageSources]);

  return <div className="flex">
    <div className="mx-auto w-full">
      <Tab.Group selectedIndex={selectedIndex} onChange={setSelectedIndex}>
        <Tab.Panels>
          {props.imageSources.map((image, index) => (<Tab.Panel key={index + props.imageSources.length}>
            {
              loading[index]
                ? (<div className="w-full aspect-square bg-gray-200 animate-pulse rounded-xl"/>)
                : (<img src={image} alt="Product Image" className="w-full aspect-square object-cover rounded-xl"/>)
            }
          </Tab.Panel>))}
        </Tab.Panels>
        <Tab.List className="flex gap-6 mt-6">
          {props.imageSources.map((image, index) => (
            <Tab key={index} className="w-1/3 rounded-md overflow-hidden ">
              {
                loading[index]
                  ? (<div className="w-full aspect-square bg-gray-200 animate-pulse"/>)
                  : (<img src={image} alt="Product Image" className="w-full aspect-square object-cover"/>)}
            </Tab>))}
        </Tab.List>
      </Tab.Group>
    </div>
  </div>;
}
import Slider from "@mui/material/Slider";
import React from "react";

export default function PriceRange(props: { numbers: number[], onChange: (event: Event, newValue: (number | number[])) => void }) {
  return <div className="px-4">
    <div className="flex justify-end">
      <span className={`text-sm text-gray-600`}>${props.numbers[0]}</span>
      <span className="text-sm text-gray-600 mx-2">-</span>
      <span className={`text-sm text-gray-600`}>${props.numbers[1]}</span>
    </div>
    <Slider className="mt-4" value={props.numbers} onChange={props.onChange} min={0} max={100} step={1}
            sx={{
              color: "#3f3f46", "& .MuiSlider-track": {
                border: "none",
              }, "&:before": {
                boxShadow: "none",
              }, "& .MuiSlider-thumb": {
                width: 8, height: 16, borderRadius: 4, "&:hover, &.Mui-focusVisible, &.Mui-active": {
                  boxShadow: "none",
                },
              },
            }}
    />
  </div>;
}
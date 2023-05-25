import React, {useEffect, useState} from 'react';

export function DoubleRangeSlider({
                                    min,
                                    max,
                                    value,
                                    onChange
                                  }: { min: number, max: number, value: number[], onChange: (value: number[]) => void }) {
  const [minValue, setMinValue] = useState(value ? value[0] : min);
  const [maxValue, setMaxValue] = useState(value ? value[1] : max);

  useEffect(() => {
    if (value) {
      setMinValue(value[0]);
      setMaxValue(value[1]);
    }
  }, [value]);

  const handleMinChange = (e: any) => {
    const value = parseInt(e.target.value);
    if (value >= min && value <= max) {
      setMinValue(value);
      onChange([value, maxValue]);
    }
  }

  const handleMaxChange = (e: any) => {
    const value = parseInt(e.target.value);
    if (value >= min && value <= max) {
      setMaxValue(value);
      onChange([minValue, value]);
    }
  }

  const minPosition = (minValue - min) / (max - min) * 100;
  const maxPosition = (maxValue - min) / (max - min) * 100;

  return (
    <div className="relative h-[4px] box-content mb-10">
      <div className="w-full h-full absolute">
        <input type="range"
               min={min}
               max={max}
               value={minValue}
               onChange={handleMinChange}
               className="absolute w-full appearance-none h-full transparent z-10 accent-transparent"
        />
        <input type="range"
               min={min}
               max={max}
               value={maxValue}
               onChange={handleMaxChange}
               className="absolute w-full appearance-none h-full transparent z-10 accent-transparent"
        />
      </div>
      <div className="w-full h-full absolute">
        <div
          className="block absolute top-1/2 h-[inherit] bg-gray-500 opacity-25 rounded-[inherit] w-full -translate-y-1/2">
        </div>
        <div className="block absolute top-1/2 h-[inherit] bg-gray-500 rounded-[inherit] -translate-y-1/2"
             style={{
               left: `${minPosition}%`,
               width: `${maxPosition - minPosition}%`
             }}
        />
        <span
          className="block absolute top-1/2 w-4 h-4 bg-current rounded-full -translate-y-1/2 -ml-2"
          style={{
            left: `${minPosition}%`,
          }}
        />
        <span
          className="block absolute top-1/2 w-4 h-4 bg-current rounded-full -translate-y-1/2 -ml-2"
          style={{
            left: `${maxPosition}%`,
          }}
        />
      </div>
    </div>
  );
}
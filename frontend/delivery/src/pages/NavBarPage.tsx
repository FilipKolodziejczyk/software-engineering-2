import { SetStateAction, useState } from 'react';
import { Link } from 'react-router-dom';


function NavBarPage  ()  {
    
    const [clickedButton, setClickedButton] = useState(0);

    const handleClick = (num: SetStateAction<number>) => {
        setClickedButton(num);
    };
   
  
    return (
      <div className="w-1/6 p-4 bg-gray-100">
            <h1 className="text-3xl font-bold text-gray-950 font-custom pt-4">Flower shop</h1>

            <div className="flex flex-col items-center mt-20">
                <Link to="/delivery/orders">
                <button
                    className={`rounded-lg py-2 px-4 mt-8 mx-auto border-2 block ${clickedButton==2 ? 'border-fuchsia-300' : ''} ${clickedButton==2 ? 'bg-fuchsia-300 text-gray-100' : 'bg-gray-100 text-gray-950'}`}
                    onClick={() => handleClick(2)}
                >
                    Orders
                </button>
                </Link>
            </div>
      </div>
    )
  };
  
  export default NavBarPage;
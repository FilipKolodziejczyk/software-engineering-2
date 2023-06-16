import { SetStateAction, useContext, useState } from 'react';
import { Link } from 'react-router-dom';
import { Context } from '../App';


function NavBarPage  ()  {
    
    const [clickedButton, setClickedButton] = useState(0);
    const { token, setToken } = useContext(Context);

    const handleClick = (num: SetStateAction<number>) => {
        setClickedButton(num);
    };
   
    const handleLogOut = () => {
        setToken('');
        setClickedButton(0);
    };
  
    return (
      <div className="w-1/6 p-4 bg-gray-100">
            <div className="h-1/6 flex flex-col items-center">
                <h1 className="text-3xl font-bold text-gray-950 font-custom pt-4">Flower shop</h1>
            </div>
            
            {token == '' ? <div></div> : <div>
            <div className="h-1/6 flex flex-col items-center">
                <Link to="/delivery/orders">
                <button
                    className={`rounded-lg py-2 px-4 mt-8 mx-auto border-2 block ${clickedButton==2 ? 'border-fuchsia-300' : ''} ${clickedButton==2 ? 'bg-fuchsia-300 text-gray-100' : 'bg-gray-100 text-gray-950'}`}
                    onClick={() => handleClick(2)}
                >
                    Orders
                </button>
                </Link>
            </div></div>
            }
            {token == '' ? <div></div> : 
            <div className="h-4/6 flex flex-col items-center justify-end">
                <Link to="/delivery/">
                <button
                    className={`rounded-lg py-2 px-4 mx-auto border-2 bg-gray-100 text-gray-950 items-end mb-3`}
                    onClick={() => handleLogOut()}
                >
                    Log out
                </button>
                </Link>
            </div>
            }
      </div>
    )
  };
  
  export default NavBarPage;
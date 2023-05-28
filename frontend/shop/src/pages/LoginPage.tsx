import { useContext, useState } from "react";
import { Context } from "../App";
import { properties } from '../resources/properties';

type Credentials = {
  username: string,
  password: string
}

export type Token = {
  jwttoken : String
}

function LoginPage  ()  {  
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [credentials, setCredentials] = useState<Credentials>({ username : "", password : "" })
  const {token, setToken} = useContext(Context);
  const [credInvalid, setCredInvalid] = useState(false)

  const handleEmailChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setCredentials({password: credentials.password, username: event.target.value})
    console.log(event.target.value)
  };
    
  const handlePasswordChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setCredentials({username: credentials.username, password: event.target.value})
    console.log(event.target.value)
  };
    
  const handleSubmit =  () => {
    console.log("handle submit")
    fetch(`${properties.url}/api/users/log_in`, {
      method: 'POST',
      body: JSON.stringify(credentials),
      headers: {
        'Content-type': 'application/json',
      },
    }).then((response) => {
      if (response.ok)
          return response.json()
      else{
        throw new Error("ERROR " + response.status)
      }
    }).then((token: Token) => {
      //console.log(token.jwttoken);
      setToken(token.jwttoken);
      console.log("Success logging in.")
      setCredInvalid(false)
    }).catch((e) => {
      setCredInvalid(true)
      console.log("Error when trying to log in: " + e)
    })
  };
    
  /*
  return (
    <div className="flex flex-col items-center justify-center h-screen w-5/6 p-4 bg-gray-300">
      <div className="bg-gray-100 p-8 rounded shadow-md">
        <form onSubmit={handleSubmit}>
          <div className="mb-4">
            <label className="block font-medium mb-2 text-gray-950">Email</label>
            <input
              type="email"
              name="email"
              value={credentials.username}
              onChange={handleEmailChange}
              className="w-full border border-gray-950 bg-gray-100 text-gray-950 p-2 rounded"
              required
            />
          </div>
          <div className="mb-4">
            <label className="block font-medium mb-2 text-gray-950">Password</label>
            <input
              type="password"
              name="password"
              value={credentials.password}
              onChange={handlePasswordChange}
              className="w-full border border-gray-950 bg-gray-100 text-gray-950 p-2 rounded"
              required
            />
          </div>
          <button type="submit" className="bg-fuchsia-300 hover:bg-gray-300 text-gray-950 font-medium py-2 px-4 rounded">
            Sign in
          </button>
        </form>
      </div>
    </div>
    )
  */
    return (
      <div className="flex flex-col items-center justify-center h-screen w-5/6 p-4 bg-gray-300">
        <div className="bg-gray-100 p-8 rounded shadow-md">
            <div className="mb-4">
              <label className="block font-medium mb-2 text-gray-950">Email</label>
              <input
                type="email"
                name="email"
                value={credentials.username}
                onChange={handleEmailChange}
                className="w-full border border-gray-950 bg-gray-100 text-gray-950 p-2 rounded"
                required
              />
            </div>
            <div className="mb-4">
              <label className="block font-medium mb-2 text-gray-950">Password</label>
              <input
                type="password"
                name="password"
                value={credentials.password}
                onChange={handlePasswordChange}
                className="w-full border border-gray-950 bg-gray-100 text-gray-950 p-2 rounded"
                required
              />
            </div>
            <button className="bg-fuchsia-300 hover:bg-gray-300 text-gray-950 font-medium py-2 px-4 rounded" onClick={()=>handleSubmit()}>
              Sign in
            </button>
        </div>
      </div>
      )
};

export default LoginPage;
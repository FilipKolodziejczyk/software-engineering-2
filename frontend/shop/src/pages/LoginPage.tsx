import { useState } from "react";

function LoginPage  ()  {  
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
    
  const handleEmailChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setEmail(event.target.value);
  };
    
  const handlePasswordChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(event.target.value);
  };
    
  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    // to do validation
    event.preventDefault();
    console.log('Email:', email);
    console.log('Password:', password);
  };
    
  
  return (
    <div className="flex flex-col items-center justify-center h-screen w-5/6 p-4 bg-gray-300">
      <div className="bg-gray-100 p-8 rounded shadow-md">
        <form onSubmit={handleSubmit}>
          <div className="mb-4">
            <label className="block font-medium mb-2 text-gray-950">Email</label>
            <input
              type="email"
              name="email"
              value={email}
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
              value={password}
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
};

    
  
      
    

export default LoginPage;
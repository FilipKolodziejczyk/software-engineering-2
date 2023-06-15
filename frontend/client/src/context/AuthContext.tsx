import {createContext, ReactNode, useContext, useEffect, useMemo, useState} from "react";
import axios from "axios";
import {User} from "../models/User";

type AuthContext = {
  token: string | null, setToken: (token: string | null) => void, user: User | null,
}

const AuthContext = createContext({} as AuthContext)

export function useAuth() {
  return useContext(AuthContext)
}

export function AuthProvider({children}: { children: ReactNode }) {
  const [token, setToken_] = useState(localStorage.getItem('token') ?? null)
  const [user, setUser] = useState<User | null>(null)

  const setToken = (token: string | null) => {
    setToken_(token)
  }

  useEffect(() => {
    if (token) {
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
      localStorage.setItem('token', token)

      axios.get(`${import.meta.env.VITE_API_BASE_URL}/api/users`).then(res => {
        console.log(res.data)
        setUser(res.data)
      }).catch(err => {
        console.log(err)
      })
    } else {
      delete axios.defaults.headers.common['Authorization']
      localStorage.removeItem('token')
    }
  }, [token]);

  const value = useMemo(() => ({token, setToken, user}), [token, user])

  return (<AuthContext.Provider value={value}>
    {children}
  </AuthContext.Provider>);
}
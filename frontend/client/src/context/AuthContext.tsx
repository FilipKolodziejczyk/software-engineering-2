import {createContext, ReactNode, useContext, useEffect, useMemo, useState} from "react";
import axios from "axios";

type AuthContext = {
  token: string | null,
  setToken: (token: string | null) => void
}

const AuthContext = createContext({} as AuthContext)

export function useAuth() {
  return useContext(AuthContext)
}

export function AuthProvider({children}: { children: ReactNode }) {
  const [token, setToken_] = useState(localStorage.getItem('token') ?? null)

  const setToken = (token: string | null) => {
    setToken_(token)
  }

  useEffect(() => {
    if (token) {
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
      localStorage.setItem('token', token)
    } else {
      delete axios.defaults.headers.common['Authorization']
      localStorage.removeItem('token')
    }
  }, [token]);

  const value = useMemo(() => ({token, setToken}), [token])

  return (
    <AuthContext.Provider value={value}>
      {children}
    </AuthContext.Provider>
  );
}
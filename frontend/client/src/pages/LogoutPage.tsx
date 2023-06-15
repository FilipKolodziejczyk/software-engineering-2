import {useAuth} from "../context/AuthContext";
import {useNavigate} from "react-router-dom";

export default function LogoutPage() {
  const {setToken} = useAuth();
  const navigate = useNavigate();

  setToken(null);
  navigate('/', {replace: true});

  return <></>;
}
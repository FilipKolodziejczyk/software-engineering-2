import {Address} from "./Address";

export type User = {
  userID: number;
  name: string;
  email: string;
  role: string;
  newsletter: boolean;
  address: Address;
}
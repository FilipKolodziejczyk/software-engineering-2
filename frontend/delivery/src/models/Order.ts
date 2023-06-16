export type Order = {
  orderID: number;
  clientID: number;
  status: String;
  address: {
    street: String,
    city: String,
    buildingNo: String,
    houseNo: String,
    postalCode: String,
    country: String
  };
};
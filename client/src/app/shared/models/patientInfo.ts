export interface patientInfo {
  firstName: string | null;
  lastName: string | null;
  phone: string | null;
  email: string;
  address: Address;
}

export interface Address {
  street: string | null;
  buildingNumber: string | number | null;
  flatNumber: string | number | null;
  postCode: string | null;
  city: string | null;
}

// export interface doctorInfo {
//   id: number;
//   firstName: string;
//   lastName: string;
//   email: string;
//   token: any;
//   photoUrl: any;
//   gender: string;
//   notesCount: number;
//   totalRating: number;
//   specialisations: any[];
//   doctorServices: any[];
// }
export interface doctorInfo {
  id: number;
  firstName: any;
  lastName: any;
  email: string;
  token: any;
  photoUrl: any;
  gender: any;
  notesCount: number;
  totalRating: number;
  specialisations: any[];
  doctorServices: any[];
  office: Office;
}

export interface Office {
  name: string;
  street: string;
  buildingNumber: string;
  flatNumber: string;
  postCode: string;
  city: string;
  shedules: Shedule[];
}

export interface Shedule {
  weekDay: string;
  hours: string[];
  visitTime: number;
}

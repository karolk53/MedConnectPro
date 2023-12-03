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
  id: number
  firstName: string
  lastName: string
  email: string
  token: any
  photoUrl: string
  gender: string
  notesCount: number
  totalRating: number
  specialisations: any[]
  doctorServices: DoctorService[]
  office: Office
}

export interface DoctorService {
  id: number
  name: string
  descripton: string
  price: number
  doctorId: number
}

export interface Office {
  name: string
  street: string
  buildingNumber: string
  flatNumber: string
  postCode: string
  city: string
  shedules: Shedule[]
}

export interface Shedule {
  id: number
  weekDay: string
  hours: string[]
  visitTime: number
}

interface FinalSchedule {
  id: number;
  weekDay: string;
  hours: { hour: string, free: boolean }[];
  visitTime: number;
}
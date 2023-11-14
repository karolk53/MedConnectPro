import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, map, Observable, ReplaySubject, switchMap } from 'rxjs';
//import { environment } from 'src/environments/environment';
import { User } from '../shared/models/user';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Token } from '@angular/compiler';
import { patientInfo } from '../shared/models/patientInfo';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  //baseUrl = environment.apiUrl;
  baseUrl = 'https://localhost:5001/api/';
  private currentUserSource = new ReplaySubject<User | null>(1);
  currentUser$ = this.currentUserSource.asObservable();
  constructor(private http: HttpClient, private router: Router) {}

  loadCurrentUser(token: string | null) {
    const jwtHelper = new JwtHelperService();

    if (token) {
      if (!jwtHelper.isTokenExpired(token)) {
        const decodedToken = jwtHelper.decodeToken(token);

        console.log('decodedToken', decodedToken);

        const user: User = {
          email: decodedToken.email,
          token: token,
          role: decodedToken.role,
          id: decodedToken.Id,
        };
        this.currentUserSource.next(user);
      } else {
        localStorage.removeItem('token');
        this.currentUserSource.next(null);
      }
    } else {
      this.currentUserSource.next(null);
    }
  }

  login(values: any) {
    const loginData = {
      Email: values.email,
      Password: values.password,
    };

    return this.http
      .post<User>(this.baseUrl + 'patients/account/login', loginData)
      .pipe(
        map((user) => {
          localStorage.setItem('token', user.token);
          this.loadCurrentUser(user.token);
        })
      );
  }

  register(values: any) {
    const registerData = {
      Email: values.email,
      Password: values.password,
    };

    return this.http
      .post<User>(this.baseUrl + 'patients/account/register', registerData)
      .pipe(
        map((user) => {
          localStorage.setItem('token', user.token);
          this.loadCurrentUser(user.token);
        })
      );
  }

  registerNewDoctor(values: any) {
    const registerNewDoctorData = {
      Email: values.email,
      Password: values.password,
      PWZ: values.pwz,
    };

    return this.http
      .post<User>(
        this.baseUrl + 'doctors/account/register',
        registerNewDoctorData
      )
      .pipe(
        map((user) => {
          localStorage.setItem('token', user.token);
          this.loadCurrentUser(user.token);
        })
      );
  }

  loginDoctor(values: any) {
    const loginDoctorData = {
      Email: values.email,
      Password: values.password,
      PWZ: values.pwz,
    };

    return this.http
      .post<User>(this.baseUrl + 'doctors/account/login', loginDoctorData)
      .pipe(
        map((user) => {
          localStorage.setItem('token', user.token);
          this.loadCurrentUser(user.token);
        })
      );
  }

  getPatientsInfo(): Observable<any> {
    return new Observable((observer) => {
      this.currentUser$.subscribe((user) => {
        if (user && user.role === 'Patient') {
          const headers = new HttpHeaders({
            Authorization: `Bearer ${user.token}`,
          });

          this.http
            .get('https://localhost:5001/api/patients/me', { headers })
            .subscribe(
              (patientInfo) => {
                observer.next(patientInfo);
                observer.complete();
              },
              (error) => {
                observer.error(error);
              }
            );
        } else {
          observer.error('Użytkownik nie jest pacjentem.');
        }
      });
    });
  }

  getDoctorInfo(): Observable<any> {
    return new Observable((observer) => {
      this.currentUser$.subscribe((doctor) => {
        if (doctor && doctor.role === 'Doctor') {
          const headers = new HttpHeaders({
            Authorization: `Bearer ${doctor.token}`,
          });

          this.http
            .get('https://localhost:5001/api/doctors/profile', { headers })
            .subscribe(
              (doctorInfo) => {
                observer.next(doctorInfo);
                observer.complete();
              },
              (error) => {
                observer.error(error);
              }
            );
        } else {
          observer.error('Użytkownik nie jest doktorem.');
        }
      });
    });
  }

  updateProfile(profileData: any): Observable<void> {
    return this.currentUser$.pipe(
      switchMap((user) => {
        if (user && user.token) {
          console.log('user.token', user.token);
          const headers = new HttpHeaders({
            Authorization: `Bearer ${user.token}`,
          });

          return this.http.put<void>(
            'https://localhost:5001/api/patients/update',
            profileData,
            { headers }
          );
        } else {
          throw new Error('User not authenticated.');
        }
      }),
      catchError((error) => {
        console.error('Error updating profile:', error);
        throw error;
      })
    );
  }

  updateAddres(newAddress: any): Observable<void> {
    return this.currentUser$.pipe(
      switchMap((user) => {
        if (user && user.token) {
          console.log('user.token', user.token);
          const headers = new HttpHeaders({
            Authorization: `Bearer ${user.token}`,
          });

          return this.http.put<void>(
            'https://localhost:5001/api/patients/address/update',
            newAddress,
            { headers }
          );
        } else {
          throw new Error('User not authenticated.');
        }
      }),
      catchError((error) => {
        console.error('Error updating profile:', error);
        throw error;
      })
    );
  }

  updateDoctorData(newData: any): Observable<void> {
    return this.currentUser$.pipe(
      switchMap((user) => {
        if (user && user.token) {
          console.log('user.token', user.token);
          const headers = new HttpHeaders({
            Authorization: `Bearer ${user.token}`,
          });

          return this.http.put<void>(
            'https://localhost:5001/api/doctors/update',
            newData,
            { headers }
          );
        } else {
          throw new Error('User not authenticated.');
        }
      }),
      catchError((error) => {
        console.error('Error updating profile:', error);
        throw error;
      })
    );
  }

  addDoctorPhoto(photo: any): Observable<void> {
    return this.currentUser$.pipe(
      switchMap((user) => {
        if (user && user.token) {
          const headers = new HttpHeaders({
            Authorization: `Bearer ${user.token}`,
          });

          const formData: FormData = new FormData();
          formData.append('file', photo);

          return this.http.post<void>(
            'https://localhost:5001/api/doctors/photo/add',
            formData,
            { headers }
          );
        } else {
          throw new Error('User not authenticated.');
        }
      }),
      catchError((error) => {
        console.error('Error adding doctor photo:', error);
        throw error;
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }
}

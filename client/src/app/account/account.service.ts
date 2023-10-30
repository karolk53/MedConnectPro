import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map, ReplaySubject } from 'rxjs';
//import { environment } from 'src/environments/environment';
import { User } from '../shared/models/user';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  //baseUrl = environment.apiUrl;
  baseUrl = 'https://localhost:5001/api/';
  private currentUserSource = new ReplaySubject<User | null>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  // loadCurrentUser(token: string | null) {
  //   if (token == null) {
  //     this.currentUserSource.next(null);
  //     return of(null);
  //   }

  //   let headers = new HttpHeaders();
  //   headers = headers.set('Authorization', `Bearer ${token}`);

  //   return this.http.get<User>(this.baseUrl + 'account', { headers }).pipe(
  //     map((user) => {
  //       if (user) {
  //         localStorage.setItem('token', user.token);
  //         this.currentUserSource.next(user);
  //         return user;
  //       } else {
  //         return null;
  //       }
  //     })
  //   );
  // }

  login(values: any) {
    const loginData = {
      Email: values.email,
      Password: values.password
    };

    return this.http.post<User>(this.baseUrl + 'patientsaccount/login', loginData).pipe(
      map((user) => {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
      })
    );
  }

  register(values: any) {
    const registerData = {
      Email: values.email,
      Password: values.password
    };

    return this.http.post<User>(this.baseUrl + 'patientsaccount/register', registerData).pipe(
      map((user) => {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
      })
    );
  }

  // logout() {
  //   localStorage.removeItem('token');
  //   this.currentUserSource.next(null);
  //   this.router.navigateByUrl('/');
  // }
}

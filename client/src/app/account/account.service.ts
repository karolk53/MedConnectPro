import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map, ReplaySubject } from 'rxjs';
//import { environment } from 'src/environments/environment';
import { User } from '../shared/models/user';
import { JwtHelperService } from '@auth0/angular-jwt';

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
          token: decodedToken.token,
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
          this.currentUserSource.next(user);
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
          this.currentUserSource.next(user);
        })
      );
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }
}

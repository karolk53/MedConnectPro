import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Office, doctorInfo } from '../shared/models/doctorInfo';

@Injectable({
  providedIn: 'root',
})
export class OfficeService {
  token = localStorage.getItem('token');

  private officeInfoSubject = new BehaviorSubject<Office | null>(null);
  officeInfo$ = this.officeInfoSubject.asObservable();

  constructor(private http: HttpClient) {
    if (this.token) {
      this.loadOfficeInfo();
    }
  }

  private loadOfficeInfo() {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${this.token}`,
    });

    this.http
      .get<doctorInfo>('https://localhost:5001/api/doctors/profile', {
        headers,
      })
      .pipe(
        tap((doctorInfo) => {
          if (doctorInfo && doctorInfo.office) {
            this.officeInfoSubject.next(doctorInfo.office);
          }
        })
      )
      .subscribe();
  }

  updateOfficeInfo(office: Office) {
    this.officeInfoSubject.next(office);
  }

  createOffice(officeData: any): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${this.token}`,
    });

    return this.http.post<any>(
      'https://localhost:5001/api/doctors/profile',
      officeData,
      { headers }
    );
  }
}

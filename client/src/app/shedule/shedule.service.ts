import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Shedule, doctorInfo } from '../shared/models/doctorInfo';

@Injectable({
  providedIn: 'root',
})
export class ScheduleService {
  private apiUrl = 'https://localhost:5001/api/shedules';
  token = localStorage.getItem('token');

  private scheduleInfoSubject = new BehaviorSubject<Shedule[] | null>(null);
  scheduleInfo$ = this.scheduleInfoSubject.asObservable();

  constructor(private http: HttpClient) {
    if (this.token) {
      this.loadScheduleInfo();
    }
  }

  private loadScheduleInfo() {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${this.token}`,
    });

    this.http
      .get<doctorInfo>('https://localhost:5001/api/doctors/profile', {
        headers,
      })
      .pipe(
        tap((scheduleInfo) => {
          this.scheduleInfoSubject.next(scheduleInfo.office.shedules);
        })
      )
      .subscribe();
  }

  updateScheduleInfo(scheduleInfo: any) {
    this.scheduleInfoSubject.next(scheduleInfo);
  }

  addSchedule(scheduleData: any): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${this.token}`,
    });

    return this.http.post<any>(this.apiUrl, scheduleData, { headers });
  }
}

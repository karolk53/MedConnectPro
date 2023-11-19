import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, tap, switchMap } from 'rxjs/operators';
import { HttpClient, HttpParams } from '@angular/common/http';
import { doctorListItem } from '../shared/models/doctorsListItem';
import { doctorInfo } from '../shared/models/doctorInfo';

@Injectable({
  providedIn: 'root',
})
export class DoctorService {
  private filtersSubject = new BehaviorSubject({
    specialization: 'All',
    name: '',
  });
  filters$: Observable<any> = this.filtersSubject.asObservable();

  private isLoadingSubject = new BehaviorSubject<boolean>(false);
  isLoading$: Observable<boolean> = this.isLoadingSubject.asObservable();

  private doctorsSubject = new BehaviorSubject<doctorListItem[]>([]);
  doctors$: Observable<doctorListItem[]> = this.doctorsSubject.asObservable();

  private sortingValue = 'None';

  constructor(private http: HttpClient) {
    // Subscribe to filters$ and update doctors list whenever filters change
    this.filters$.subscribe((filters) => {
      this.updateDoctorsList();
    });
  }

  setFilters(filters: any) {
    this.filtersSubject.next(filters);
  }

  setIsLoading(isLoading: boolean) {
    this.isLoadingSubject.next(isLoading);
  }

  setSorting(sorting: string) {
    this.sortingValue = sorting;
    this.updateDoctorsList();
  }

  getDoctors(): Observable<doctorListItem[]> {
    // Use switchMap to switch to the new observable based on the emitted filters
    return this.filters$.pipe(
      switchMap((filters) => {
        let params = new HttpParams();
        if (filters.specialization && filters.specialization !== 'All') {
          params = params.set('specialisation', filters.specialization);
        }

        if (this.sortingValue !== 'None') {
          params = params.set('SortByTotalRating', this.sortingValue);
        }

        this.setIsLoading(true);

        return this.http
          .get<doctorListItem[]>('https://localhost:5001/api/doctors', {
            params,
          })
          .pipe(
            tap((doctors) => {
              this.doctorsSubject.next(doctors);
              this.setIsLoading(false);
            }),
            catchError((error) => {
              this.setIsLoading(false);
              return throwError(error);
            })
          );
      })
    );
  }

  updateDoctorsList(): void {
    if (!this.isLoadingSubject.value) {
      // Use switchMap to switch to the new observable based on the emitted filters
      this.filters$.pipe(switchMap((filters) => this.getDoctors())).subscribe(
        () => {
          this.setIsLoading(false); // Set loading state after fetching doctors
        },
        (error) => {
          this.setIsLoading(false); // Set loading state on error as well
          console.error('Error updating doctors list', error);
        }
      );
    } else {
      console.log(
        'Another operation is in progress. Cannot update doctors list.'
      );
    }
  }

  getDoctor(id: number) {
    return this.http.get<doctorInfo>(
      'https://localhost:5001/api/doctors/' + id
    );
  }
}

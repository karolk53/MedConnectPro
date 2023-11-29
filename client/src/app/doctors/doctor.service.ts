import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, tap, switchMap } from 'rxjs/operators';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { doctorListItem } from '../shared/models/doctorsListItem';
import { doctorInfo } from '../shared/models/doctorInfo';
import { CommentModule } from '../shared/models/comment';

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

  private commentsSubject = new BehaviorSubject<CommentModule[]>([]);
  comments$: Observable<CommentModule[]> = this.commentsSubject.asObservable();

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

  addDoctorNote(doctorId: number, note: any): Observable<any> {
    const endpoint = `https://localhost:5001/api/notes/${doctorId}`;
    
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`, // Zastąp 'your_token_key' odpowiednim kluczem z local storage
    });

    return this.http.post(endpoint, note, { headers }).pipe(
      tap(() => {
        // Dodaj tutaj dodatkowe akcje po pomyślnym dodaniu oceny, jeśli są potrzebne
        console.log('Note added successfully');
      }),
      catchError((error) => {
        console.error('Error adding note', error);
        return throwError(error);
      })
    );
  }

  getComments(doctorId: number): Observable<CommentModule[]> {
    const url = `https://localhost:5001/api/notes/${doctorId}`;
    return this.http.get<CommentModule[]>(url);
  }

  updateComments(doctorId: number, newComments: Comment[]): Observable<CommentModule[]> {
    const url = `https://localhost:5001/api/notes/${doctorId}`;
    return this.http.put<CommentModule[]>(url, newComments).pipe(
      // Aktualizuj BehaviorSubject po zaktualizowaniu komentarzy
      tap(() => this.loadComments(doctorId))
    );
  }

  loadComments(doctorId: number): void {
    this.getComments(doctorId).subscribe(
      comments => this.commentsSubject.next(comments.reverse()),
      error => console.error('Error fetching comments', error)
    );
  }
}

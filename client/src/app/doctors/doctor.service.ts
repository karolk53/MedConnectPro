import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

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

  setFilters(filters: any) {
    this.filtersSubject.next(filters);
  }

  setIsLoading(isLoading: boolean) {
    this.isLoadingSubject.next(isLoading);
  }

  constructor() {}
}

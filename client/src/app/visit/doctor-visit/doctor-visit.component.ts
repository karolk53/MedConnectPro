import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-doctor-visit',
  templateUrl: './doctor-visit.component.html',
  styleUrls: ['./doctor-visit.component.scss'],
})
export class DoctorVisitComponent {
  apiUrl = 'https://localhost:5001/api/doctors/visits';
  visits: any[] = [];
  isLoading = false;

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.getPatientVisits();
  }

  getPatientVisits(filters: any = {}) {
    const token = localStorage.getItem('token');
    this.isLoading = true;
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });

    this.http
      .get<any[]>(this.apiUrl, { headers: headers, params: filters })
      .subscribe(
        (data) => {
          this.visits = data;
          console.log('doctors visits:', this.visits);
          this.isLoading = false;
        },
        (error) => {
          console.error('Error fetching doctors visits:', error);
          this.isLoading = false;
        }
      );
  }

  navigateToVisit(id: number) {
    const queryParams = {
      id: id,
    };

    this.router.navigate(['/visit/item'], { queryParams: queryParams });
  }

  filtersForm = new FormGroup({
    Status: new FormControl('All', [Validators.required]),
    Name: new FormControl(''),
    Date: new FormControl(''),
  });

  changeFilters() {
    if (this.filtersForm.valid) {
      const newFilter: {
        Status?: string | null;
        Name?: string | null;
        Date?: string | null;
      } = {};
      if (this.filtersForm.value.Status !== 'All') {
        newFilter.Status = this.filtersForm.value.Status;
      }
      if (this.filtersForm.value.Name !== '') {
        newFilter.Name = this.filtersForm.value.Name;
      }
      if (this.filtersForm.value.Date !== '') {
        newFilter.Date = this.filtersForm.value.Date;
      }
      console.log('newFilter', newFilter);
      this.getPatientVisits(newFilter);
    } else {
      console.log('Form is invalid. Please fill in all required fields.');
    }
  }
}

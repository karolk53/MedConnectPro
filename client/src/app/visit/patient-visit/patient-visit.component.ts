import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-patient-visit',
  templateUrl: './patient-visit.component.html',
  styleUrls: ['./patient-visit.component.scss']
})
export class PatientVisitComponent implements OnInit {
  apiUrl = 'https://localhost:5001/api/patients/visits';
  visits: any[] = [];
  isLoading = false;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getPatientVisits();
  }

  filtersForm = new FormGroup({
    Status: new FormControl('All', [Validators.required]),
    Name: new FormControl(''),
    Date: new FormControl(''),
  });

  getPatientVisits(filters: any = {}) {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    this.http.get<any[]>(this.apiUrl, { headers: headers, params: filters }).subscribe(
      (data) => {
        this.visits = data;
        console.log('Patient visits:', this.visits);
      },
      (error) => {
        console.error('Error fetching patient visits:', error);
      }
    );
  }

  changeFilters(){
    if (this.filtersForm.valid) {
      const newFilter: { Status?: string | null; Name?: string | null; Date?: string | null } = {};
      if (this.filtersForm.value.Status !== "All") {
        newFilter.Status = this.filtersForm.value.Status;
      }
      if (this.filtersForm.value.Name !== "") {
        newFilter.Name = this.filtersForm.value.Name;
      }
      if (this.filtersForm.value.Date !== "") {
        newFilter.Date = this.filtersForm.value.Date;
      }
      console.log('newFilter', newFilter)
      this.getPatientVisits(newFilter);
    } else {
      console.log('Form is invalid. Please fill in all required fields.');
    }
  
}
}

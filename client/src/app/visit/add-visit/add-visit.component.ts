import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-add-visit',
  templateUrl: './add-visit.component.html',
  styleUrls: ['./add-visit.component.scss'],
})
export class AddVisitComponent implements OnInit {
  apiUrl = 'https://localhost:5001/api/visits';
  hour: string = '';
  date: string = '';
  doctorId: string = '';
  isSucces = false;
  isError = false;

  constructor(private route: ActivatedRoute, private http: HttpClient) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.hour = params['hour'] || '';
      this.date = params['date'] || '';
      this.doctorId = params['doctorId'] || '';

      this.updateFormValues(); // Aktualizuj formularz po otrzymaniu wartości
    });
  }

  visitForm = new FormGroup({
    hour: new FormControl('', [Validators.required]),
    date: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required),
  });

  private updateFormValues() {
    this.visitForm.patchValue({
      hour: this.hour,
      date: this.date,
    });
  }

  onSubmit() {
    if (this.visitForm.valid) {
      const note = this.visitForm.value.description;
      const plannedDate = `${this.visitForm.value.date} ${this.visitForm.value.hour}`;

      const token = localStorage.getItem('token');
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      });

      const requestBody = {
        Note: note,
        PlannedDate: plannedDate,
      };

      this.http
        .post(`${this.apiUrl}/${this.doctorId}`, requestBody, {
          headers: headers,
        })
        .subscribe(
          (response) => {
            console.log('Visit added successfully:', response);
            this.isSucces = true;
          },
          (error) => {
            console.error('Error adding visit:', error);
            this.isError = true;
          }
        );
    }
  }
}

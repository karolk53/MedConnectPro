import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-doctor-visit-item',
  templateUrl: './doctor-visit-item.component.html',
  styleUrls: ['./doctor-visit-item.component.scss']
})
export class DoctorVisitItemComponent {
  visitId = "";
  apiUrl = 'https://localhost:5001/api/visits';
  token = localStorage.getItem('token');
  headers = new HttpHeaders({
    'Authorization': `Bearer ${this.token}`
  });

  isStarted = false;
  isCanceled = false;
  isEnded = false;

  visit: any;

  constructor(private route: ActivatedRoute, private http: HttpClient) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.visitId = params['id'] || "";
    });
    this.loadVisitDetails(this.visitId);
  }

  loadVisitDetails(visitId: string) {
    const url = `${this.apiUrl}/${visitId}`;


    this.http.get<any>(url,{ headers: this.headers }).subscribe(
      (data) => {
        console.log('visit', data)
        this.visit = data;
      },
      (error) => {
        console.error('Error fetching visit details:', error);
      }
    );
  }

  startVisit() {
    if (this.visit) {
      const startUrl = `${this.apiUrl}/start/${this.visitId}`;
      this.http.put(startUrl, {},{ headers: this.headers }).subscribe(
        (response) => {
          console.log('Visit started successfully:', response);
          this.isStarted = true;
          this.loadVisitDetails(this.visitId);
        },
        (error) => {
          console.error('Error starting visit:', error);
        }
      );
    }
  }

  cancelVisit() {
    if (this.visit) {
      const startUrl = `${this.apiUrl}/cancel/${this.visitId}`;
      this.http.put(startUrl, {},{ headers: this.headers }).subscribe(
        (response) => {
          console.log('Visit started successfully:', response);
          this.isCanceled = true;
          this.loadVisitDetails(this.visitId);
        },
        (error) => {
          console.error('Error starting visit:', error);
        }
      );
    }
  }

  endVisit() {
    if (this.visit) {
      const startUrl = `${this.apiUrl}/end/${this.visitId}`;
      this.http.put(startUrl, {},{ headers: this.headers }).subscribe(
        (response) => {
          console.log('Visit started successfully:', response);
          this.isEnded = true;
          this.loadVisitDetails(this.visitId);
        },
        (error) => {
          console.error('Error starting visit:', error);
        }
      );
    }
  }
}

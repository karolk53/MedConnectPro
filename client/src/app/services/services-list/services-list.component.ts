import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DoctorService, doctorInfo } from 'src/app/shared/models/doctorInfo';

@Component({
  selector: 'app-services-list',
  templateUrl: './services-list.component.html',
  styleUrls: ['./services-list.component.scss']
})
export class ServicesListComponent {

  services: DoctorService[] = []; 

  isLoading = false;
  isSucces = false;
  isError = false;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getServices();
  }

  servicesForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    price: new FormControl('', Validators.required),
  });

  getServices() {
    const endpoint = 'https://localhost:5001/api/doctors/profile';
    const token = localStorage.getItem('token');
  
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    });
  
    this.http.get<doctorInfo>(endpoint, { headers }).subscribe(
      (response) => {
        this.services = response.doctorServices.reverse();
        console.log('Services retrieved successfully:', this.services);
      },
      (error) => {
        console.error('Error retrieving services:', error);
      }
    );
  }

  addService() {
    this.isLoading = true;
    this.isSucces = false;
    this.isError = false;
  

    const endpoint = 'https://localhost:5001/api/doctors/services';
    
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    });

    const service = {
      Name: this.servicesForm.get('name')?.value,
      Descripton: this.servicesForm.get('description')?.value,
      Price: this.servicesForm.get('price')?.value
    };

    this.http.post(endpoint, service, { headers }).subscribe(
      (response) => {
        console.log('Service added successfully:', response);
        this.getServices();
        this.isSucces = true;
        this.isLoading = false;

      },
      (error) => {
        console.error('Error adding service:', error);
        this.isError = true;
        this.isLoading = false;

      }
    );
  }

  deleteService(serviceId:number){
    const endpoint = `https://localhost:5001/api/doctors/services/delete/${serviceId}`;
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    });

    this.http.delete(endpoint, { headers }).subscribe(
      (response) => {
        console.log('Service added successfully:', response);
        this.getServices();
      },
      (error) => {
        console.error('Error adding service:', error);
      }
    );
  }

}

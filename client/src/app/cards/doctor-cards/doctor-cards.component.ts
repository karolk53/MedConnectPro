import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-doctor-cards',
  templateUrl: './doctor-cards.component.html',
  styleUrls: ['./doctor-cards.component.scss'],
})
export class DoctorCardsComponent {
  cards: any[] = [
    {
      id: 1,
      patientId: 2,
      patientName: 'Jan Kowalski',
      patientPESEL: '81110642153',
      creationDate: '2023-12-15',
      lastVisitDate: '2023-12-20T15:30:00',
    },
    {
      id: 2,
      patientId: 2,
      patientName: 'Adam Nowak',
      patientPESEL: '81110642153',
      creationDate: '2023-12-15',
      lastVisitDate: '2023-12-20T15:30:00',
    },
    {
      id: 3,
      patientId: 2,
      patientName: 'Jan Grzech',
      patientPESEL: '81110642153',
      creationDate: '2023-12-15',
      lastVisitDate: '2023-12-20T15:30:00',
    },
    {
      id: 4,
      patientId: 2,
      patientName: 'Bartłomiej Grzech',
      patientPESEL: '81110642153',
      creationDate: '2023-12-15',
      lastVisitDate: '2023-12-20T15:30:00',
    },
    {
      id: 5,
      patientId: 2,
      patientName: 'Anna nowak',
      patientPESEL: '81110642153',
      creationDate: '2023-12-15',
      lastVisitDate: '2023-12-20T15:30:00',
    },
  ];
  isLoading = false;

  constructor(private http: HttpClient, private router: Router) {}

  filtersForm = new FormGroup({
    Name: new FormControl(''),
    Pesel: new FormControl(''),
  });

  navigateToVisit(id: number) {
    const queryParams = {
      id: id,
    };

    this.router.navigate(['/visit/item'], { queryParams: queryParams });
  }

  changeFilters() {
    if (this.filtersForm.valid) {
      const newFilter: {
        Name?: string | null;
        Pesel?: string | null;
      } = {};
      if (this.filtersForm.value.Name !== '') {
        newFilter.Name = this.filtersForm.value.Name;
      }
      if (this.filtersForm.value.Pesel !== '') {
        newFilter.Pesel = this.filtersForm.value.Pesel;
      }
      console.log('newFilter', newFilter);
    } else {
      console.log('Form is invalid. Please fill in all required fields.');
    }
  }
}

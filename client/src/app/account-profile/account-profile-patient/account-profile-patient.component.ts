import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { patientInfo } from 'src/app/shared/models/patientInfo';

@Component({
  selector: 'app-account-profile-patient',
  templateUrl: './account-profile-patient.component.html',
  styleUrls: ['./account-profile-patient.component.scss'],
})
export class AccountProfilePatientComponent implements OnInit {
  data: null | patientInfo = null;

  constructor(private accountService: AccountService) {}

  ngOnInit() {
    this.accountService.getPatientsInfo().subscribe(
      (patientInfo) => {
        this.data = patientInfo;
        console.log('Dane pacjenta:', this.data);
      },
      (error) => {
        console.error('Błąd podczas pobierania danych pacjenta:', error);
      }
    );
  }
}

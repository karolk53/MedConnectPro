import { Component } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { doctorInfo } from 'src/app/shared/models/doctorInfo';

@Component({
  selector: 'app-account-profile-doctor',
  templateUrl: './account-profile-doctor.component.html',
  styleUrls: ['./account-profile-doctor.component.scss'],
})
export class AccountProfileDoctorComponent {
  data: null | doctorInfo = null;

  constructor(private accountService: AccountService) {}

  ngOnInit() {
    this.accountService.getDoctorInfo().subscribe(
      (patientInfo) => {
        this.data = patientInfo;
        console.log('Dane doktora:', this.data);
      },
      (error) => {
        console.error('Błąd podczas pobierania danych pacjenta:', error);
      }
    );
  }
}

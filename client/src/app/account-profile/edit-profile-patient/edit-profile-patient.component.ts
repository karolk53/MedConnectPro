import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from 'src/app/account/account.service';
import { User } from 'src/app/shared/models/user';

@Component({
  selector: 'app-edit-profile-patient',
  templateUrl: './edit-profile-patient.component.html',
  styleUrls: ['./edit-profile-patient.component.scss'],
})
export class EditProfilePatientComponent implements OnInit {
  userData: User | null = null;
  public isSumbitingData = false;
  public isSumbitingDataSucces = false;
  public isSumbitingDataError = false;

  public isSumbitingAddressData = false;
  public isSumbitingAddressDataSucces = false;
  public isSumbitingAddressDataError = false;


  constructor(
    private accountService: AccountService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this.accountService.currentUser$.subscribe((user: User | null) => {
      this.userData = user;

      this.patientDataForm.controls['email'].setValue(
        this.userData?.email || ''
      );
    });
  }

  patientDataForm = new FormGroup({
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl('', Validators.required),
    phone: new FormControl('', Validators.required),
    gender: new FormControl('', Validators.required),
    pesel: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
  });

  patientAddressForm = new FormGroup({
    street: new FormControl('', [Validators.required]),
    buildingNumber: new FormControl('', [Validators.required]),
    flatNumber: new FormControl('', [Validators.required]),
    postCode: new FormControl('', [Validators.required]),
    city: new FormControl('', [Validators.required]),
  });

  onSubmitPatientData() {
    this.isSumbitingData = true;
    this.isSumbitingDataError = false;
    this.isSumbitingDataSucces = false;


    let newData = {
      "FirstName": this.patientDataForm.value.firstName,
      "LastName": this.patientDataForm.value.lastName,
      "Email": this.patientDataForm.value.email,
      "Phone": this.patientDataForm.value.phone,
      "PESEL": this.patientDataForm.value.pesel,
      "Gender": this.patientDataForm.value.gender
    }
  
    console.log('submit1', newData);
  
    this.accountService.updateProfile(newData)
      .subscribe(
        () => {
          console.log('Profile updated successfully!');
          this.isSumbitingData = false;
          this.isSumbitingDataSucces = true;
        },
        (error) => {
          console.error('Error updating profile:', error);
          this.isSumbitingData = false;
          this.isSumbitingDataError = true;
        }
      );
  }

  async onSubmitPatientAddres() {
    this.isSumbitingAddressData = true;
    this.isSumbitingAddressDataError = false;
    this.isSumbitingAddressDataSucces = false;


    let newAddress = {
      "Street": this.patientAddressForm.value.street,
      "BuildingNumber": this.patientAddressForm.value.buildingNumber,
      "FlatNumber": this.patientAddressForm.value.flatNumber,
      "PostCode": this.patientAddressForm.value.postCode,
      "City": this.patientAddressForm.value.city
    }
  
    console.log('submit2', newAddress);
  
    this.accountService.updateAddres(newAddress)
      .subscribe(
        () => {
          console.log('Addres updated successfully!');
          this.isSumbitingAddressData = false;
          this.isSumbitingAddressDataSucces = true;
        },
        (error) => {
          console.error('Error updating addres:', error);
          this.isSumbitingAddressData = false;
          this.isSumbitingAddressDataError = true;
        }
      );
  }

}

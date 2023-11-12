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

  onSubmit() {
    console.log('submit');
  }
}

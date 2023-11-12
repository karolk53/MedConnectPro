import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountProfileDoctorComponent } from './account-profile-doctor/account-profile-doctor.component';
import { AccountProfilePatientComponent } from './account-profile-patient/account-profile-patient.component';
import { AccountProfileRoutingModule } from './account-profile-routing.module';
import { EditProfilePatientComponent } from './edit-profile-patient/edit-profile-patient.component';
import { SharedModule } from '../shared/shared.module';
import { EditProfileDoctorComponent } from './edit-profile-doctor/edit-profile-doctor.component';

@NgModule({
  declarations: [
    AccountProfileDoctorComponent,
    AccountProfilePatientComponent,
    EditProfilePatientComponent,
    EditProfileDoctorComponent,
  ],
  imports: [CommonModule, AccountProfileRoutingModule, SharedModule],
})
export class AccountProfileModule {}

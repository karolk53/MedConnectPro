import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountProfilePatientComponent } from './account-profile-patient/account-profile-patient.component';
import { AccountProfileDoctorComponent } from './account-profile-doctor/account-profile-doctor.component';
import { RouterModule } from '@angular/router';
import { authPatientGuard } from '../core/guards/auth-patient.guard';
import { EditProfilePatientComponent } from './edit-profile-patient/edit-profile-patient.component';
import { authDoctorGuard } from '../core/guards/auth-doctor.guard';
import { EditProfileDoctorComponent } from './edit-profile-doctor/edit-profile-doctor.component';

const routes = [
  {
    path: 'profile-patient',
    canActivate: [authPatientGuard],
    component: AccountProfilePatientComponent,
    data: { breadcrumb: 'Patient Profile' },
  },
  {
    path: 'edit-patient-profile',
    canActivate: [authPatientGuard],
    component: EditProfilePatientComponent,
    data: { breadcrumb: 'Edit Patient Profile' },
  },
  {
    path: 'profile-doctor',
    canActivate: [authDoctorGuard],
    component: AccountProfileDoctorComponent,
    data: { breadcrumb: 'Doctor Profile' },
  },
  {
    path: 'edit-profile-doctor',
    canActivate: [authDoctorGuard],
    component: EditProfileDoctorComponent,
    data: { breadcrumb: 'Edit Doctor Profile' },
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AccountProfileRoutingModule {}

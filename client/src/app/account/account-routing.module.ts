import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { RegisterDoctorComponent } from './register-doctor/register-doctor.component';
import { LoginDoctorComponent } from './login-doctor/login-doctor.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: 'login-doctor',
    component: LoginDoctorComponent,
    data: { breadcrumb: 'Login as doctor' },
  },
  {
    path: 'register-doctor',
    component: RegisterDoctorComponent,
    data: { breadcrumb: 'Register as doctor' },
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AccountRoutingModule {}

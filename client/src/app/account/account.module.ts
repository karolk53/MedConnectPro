import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from '../shared/shared.module';
import { LoginDoctorComponent } from './login-doctor/login-doctor.component';
import { RegisterDoctorComponent } from './register-doctor/register-doctor.component';

@NgModule({
  declarations: [LoginComponent, RegisterComponent, LoginDoctorComponent, RegisterDoctorComponent],
  imports: [CommonModule, AccountRoutingModule, SharedModule],
})
export class AccountModule {}

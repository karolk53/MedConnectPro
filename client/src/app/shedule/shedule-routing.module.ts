import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SheduleEditComponent } from './shedule-edit/shedule-edit.component';
import { Routes } from '@angular/router';
import { SheduleDoctorComponent } from './shedule-doctor/shedule-doctor.component';

const routes: Routes = [
  { path: '', component: SheduleDoctorComponent, data: { breadcrumb: 'Shedule' } },
  { path: '', component: SheduleEditComponent },

];

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class SheduleRoutingModule { }

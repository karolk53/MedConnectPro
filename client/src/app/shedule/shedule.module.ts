import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SheduleEditComponent } from './shedule-edit/shedule-edit.component';
import { SheduleDoctorComponent } from './shedule-doctor/shedule-doctor.component';



@NgModule({
  declarations: [
    SheduleEditComponent,
    SheduleDoctorComponent
  ],
  imports: [
    CommonModule
  ]
})
export class SheduleModule { }

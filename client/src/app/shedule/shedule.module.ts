import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SheduleEditComponent } from './shedule-edit/shedule-edit.component';
import { SheduleDoctorComponent } from './shedule-doctor/shedule-doctor.component';
import { SheduleRoutingModule } from './shedule-routing.module';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [SheduleEditComponent, SheduleDoctorComponent],
  imports: [CommonModule, SheduleRoutingModule, SharedModule],
})
export class SheduleModule {}

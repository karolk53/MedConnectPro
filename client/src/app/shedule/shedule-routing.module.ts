import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SheduleDoctorComponent } from './shedule-doctor/shedule-doctor.component';
import { SheduleEditComponent } from './shedule-edit/shedule-edit.component';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: SheduleDoctorComponent,
    data: { breadcrumb: 'Shedule' },
  },
  {
    path: 'edit',
    component: SheduleEditComponent,
    data: { breadcrumb: 'Edit Shedule' },
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SheduleRoutingModule {}

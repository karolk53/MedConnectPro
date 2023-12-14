import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddVisitComponent } from './add-visit/add-visit.component';
import { RouterModule, Routes } from '@angular/router';
import { PatientVisitComponent } from './patient-visit/patient-visit.component';
import { DoctorVisitComponent } from './doctor-visit/doctor-visit.component';
import { DoctorVisitItemComponent } from './doctor-visit-item/doctor-visit-item.component';

const routes: Routes = [
  {
    path: 'add',
    component: AddVisitComponent,
    data: { breadcrumb: 'Add visit' },
  },
  {
    path: 'patient',
    component: PatientVisitComponent,
    data: { breadcrumb: 'Patient visits' },
  },
  {
    path: 'doctor',
    component: DoctorVisitComponent,
    data: { breadcrumb: 'Doctor visits' },
  },
  {
    path: 'item',
    component: DoctorVisitItemComponent,
    data: { breadcrumb: 'Doctor visits' },
  },
]

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],

})
export class VisitRoutingModule { }

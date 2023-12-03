import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddVisitComponent } from './add-visit/add-visit.component';
import { VisitRoutingModule } from './visit-routing.module';
import { SharedModule } from '../shared/shared.module';
import { PatientVisitComponent } from './patient-visit/patient-visit.component';
import { DoctorVisitComponent } from './doctor-visit/doctor-visit.component';
import { DoctorVisitItemComponent } from './doctor-visit-item/doctor-visit-item.component';



@NgModule({
  declarations: [
    AddVisitComponent,
    PatientVisitComponent,
    DoctorVisitComponent,
    DoctorVisitItemComponent
  ],
  imports: [CommonModule, SharedModule, VisitRoutingModule],

})
export class VisitModule { }

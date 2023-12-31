import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DoctorsListComponent } from './doctors-list/doctors-list.component';
import { DoctorsItemComponent } from './doctors-item/doctors-item.component';
import { SharedModule } from '../shared/shared.module';
import { DotorsFiltersComponent } from './dotors-filters/dotors-filters.component';
import { DoctorsRoutingModule } from './doctors-routing.module';
import { DoctorReviewsComponent } from './doctor-reviews/doctor-reviews.component';

@NgModule({
  declarations: [
    DoctorsListComponent,
    DoctorsItemComponent,
    DotorsFiltersComponent,
    DoctorReviewsComponent,
  ],
  imports: [CommonModule, SharedModule, DoctorsRoutingModule],
})
export class DoctorsModule {}

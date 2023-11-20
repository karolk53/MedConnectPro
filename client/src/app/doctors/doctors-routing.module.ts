import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DoctorsListComponent } from './doctors-list/doctors-list.component';
import { DoctorsItemComponent } from './doctors-item/doctors-item.component';

const routes: Routes = [
  {
    path: '',
    component: DoctorsListComponent,
    data: { breadcrumb: 'Doctos list' },
  },
  {
    path: ':id',
    component: DoctorsItemComponent,
    data: { breadcrumb: { alias: 'Doctor' } },
  },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class DoctorsRoutingModule {}

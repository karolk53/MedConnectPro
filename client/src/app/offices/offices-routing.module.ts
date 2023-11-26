import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { EditOfficeComponent } from './edit-office/edit-office.component';
import { ShowOfficeComponent } from './show-office/show-office.component';

const routes: Routes = [
  {
    path: '',
    component: ShowOfficeComponent,
    data: { breadcrumb: 'Office' },
  },
  {
    path: 'edit',
    component: EditOfficeComponent,
    data: { breadcrumb: { alias: 'Doctor' } },
  },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class OfficesRoutingModule {}

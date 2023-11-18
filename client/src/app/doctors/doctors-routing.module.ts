import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DoctorsListComponent } from './doctors-list/doctors-list.component';
import { DoctorsItemComponent } from './doctors-item/doctors-item.component';

const routes: Routes = [
  { path: '', component: DoctorsListComponent },
  { path: 'item', component: DoctorsItemComponent },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class DoctorsRoutingModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DoctorCardsComponent } from './doctor-cards/doctor-cards.component';

const routes: Routes = [
  {
    path: '',
    component: DoctorCardsComponent,
    data: { breadcrumb: 'Cards' },
  },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class CardsRoutingModule {}

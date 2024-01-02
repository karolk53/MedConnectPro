import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DoctorCardsComponent } from './doctor-cards/doctor-cards.component';
import { CardsRoutingModule } from './cards-routing.module';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [DoctorCardsComponent],
  imports: [CommonModule, CardsRoutingModule, SharedModule, CommonModule],
})
export class CardsModule {}

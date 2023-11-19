import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { NavLinkComponent } from './components/nav-link/nav-link.component';
import { RouterModule } from '@angular/router';
import { TextInputComponent } from './components/text-input/text-input.component';
import { SubmitButtonComponent } from './components/submit-button/submit-button.component';
import { DoctorListItemComponent } from './components/doctor-list-item/doctor-list-item.component';
import { RatingStarComponent } from './components/rating-star/rating-star.component';

@NgModule({
  declarations: [
    NavLinkComponent,
    TextInputComponent,
    SubmitButtonComponent,
    DoctorListItemComponent,
    RatingStarComponent,
  ],
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  exports: [
    ReactiveFormsModule,
    NavLinkComponent,
    TextInputComponent,
    SubmitButtonComponent,
    DoctorListItemComponent,
    RatingStarComponent,
  ],
})
export class SharedModule {}

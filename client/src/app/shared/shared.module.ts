import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { NavLinkComponent } from './components/nav-link/nav-link.component';
import { RouterModule } from '@angular/router';
import { TextInputComponent } from './components/text-input/text-input.component';
import { SubmitButtonComponent } from './components/submit-button/submit-button.component';

@NgModule({
  declarations: [NavLinkComponent, TextInputComponent, SubmitButtonComponent],
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  exports: [
    ReactiveFormsModule,
    NavLinkComponent,
    TextInputComponent,
    SubmitButtonComponent,
  ],
})
export class SharedModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ServicesListComponent } from './services-list/services-list.component';
import { SharedModule } from '../shared/shared.module';
import { DoctorsRoutingModule } from '../doctors/doctors-routing.module';
import { ServicesRoutingModule } from './services-routing.module';



@NgModule({
  declarations: [
    ServicesListComponent
  ],
  imports: [CommonModule, SharedModule, ServicesRoutingModule],

  
})
export class ServicesModule { }

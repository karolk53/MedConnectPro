import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditOfficeComponent } from './edit-office/edit-office.component';
import { ShowOfficeComponent } from './show-office/show-office.component';
import { SharedModule } from '../shared/shared.module';
import { OfficesRoutingModule } from './offices-routing.module';

@NgModule({
  declarations: [EditOfficeComponent, ShowOfficeComponent],
  imports: [CommonModule, SharedModule, OfficesRoutingModule],
})
export class OfficesModule {}

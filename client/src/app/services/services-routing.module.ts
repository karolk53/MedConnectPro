import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ServicesListComponent } from './services-list/services-list.component';

const routes: Routes = [
  {
    path: '',
    component: ServicesListComponent,
    data: { breadcrumb: 'Services list' },
    
  }
]

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class ServicesRoutingModule { }

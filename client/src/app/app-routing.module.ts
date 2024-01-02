import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DoctorComponent } from './doctor/doctor.component';

const routes: Routes = [
  { path: '', component: HomeComponent, data: { breadcrumb: 'Home' } },
  { path: 'doctor', component: DoctorComponent },
  {
    path: 'account',
    loadChildren: () =>
      import('./account/account.module').then((m) => m.AccountModule),
  },
  {
    path: 'profile',
    loadChildren: () =>
      import('./account-profile/account-profile.module').then(
        (m) => m.AccountProfileModule
      ),
  },
  {
    path: 'doctors',
    loadChildren: () =>
      import('./doctors/doctors.module').then((m) => m.DoctorsModule),
  },
  {
    path: 'shedule',
    loadChildren: () =>
      import('./shedule/shedule.module').then((m) => m.SheduleModule),
  },
  {
    path: 'office',
    loadChildren: () =>
      import('./offices/offices.module').then((m) => m.OfficesModule),
  },
  {
    path: 'services',
    loadChildren: () =>
      import('./services/services.module').then((m) => m.ServicesModule),
  },
  {
    path: 'visit',
    loadChildren: () =>
      import('./visit/visit.module').then((m) => m.VisitModule),
  },
  {
    path: 'cards',
    loadChildren: () =>
      import('./cards/cards.module').then((m) => m.CardsModule),
  },

  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

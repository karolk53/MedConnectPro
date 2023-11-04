import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { NavLinkComponent } from '../shared/components/nav-link/nav-link.component';
import { SharedModule } from '../shared/shared.module';
import { SectionHeaderComponent } from './section-header/section-header.component';
import { BreadcrumbModule } from 'xng-breadcrumb';

@NgModule({
  declarations: [NavBarComponent, SectionHeaderComponent],
  imports: [CommonModule, RouterModule, SharedModule, BreadcrumbModule],
  exports: [NavBarComponent, SectionHeaderComponent],
})
export class CoreModule {}

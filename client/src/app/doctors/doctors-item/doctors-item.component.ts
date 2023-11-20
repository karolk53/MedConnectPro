import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { DoctorService } from '../doctor.service';
import { doctorInfo } from 'src/app/shared/models/doctorInfo';

@Component({
  selector: 'app-doctors-item',
  templateUrl: './doctors-item.component.html',
  styleUrls: ['./doctors-item.component.scss'],
})
export class DoctorsItemComponent {
  isLoading = true;
  doctor?: doctorInfo;

  constructor(
    private activatedRoute: ActivatedRoute,
    private bcService: BreadcrumbService,
    private doctorService: DoctorService
  ) {
    this.bcService.set('@Doctor', '');
  }

  ngOnInit(): void {
    this.loadDoctor();
  }

  loadDoctor() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id)
      this.doctorService.getDoctor(+id).subscribe({
        next: (doctor) => {
          this.doctor = doctor;
          this.bcService.set('@Doctor', doctor.email);
          this.isLoading = false;
          console.log('doctor', doctor);
        },
        error: (error) => console.log(error),
      });
  }
}

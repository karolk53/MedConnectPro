import { Component } from '@angular/core';
import { DoctorService } from '../doctor.service';
import { doctorListItem } from 'src/app/shared/models/doctorsListItem';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-doctors-list',
  templateUrl: './doctors-list.component.html',
  styleUrls: ['./doctors-list.component.scss'],
})
export class DoctorsListComponent {
  doctors: doctorListItem[] = [];
  isLoading$: Observable<boolean>;

  constructor(private doctorService: DoctorService) {
    this.isLoading$ = doctorService.isLoading$;
  }

  ngOnInit() {
    this.doctorService.doctors$.subscribe((doctors) => {
      this.doctors = doctors;
    });
  }

  onSortingChange(event: Event) {
    const selectedSorting = (event.target as HTMLSelectElement).value;
    this.doctorService.setSorting(selectedSorting);
  }
}

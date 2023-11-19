import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DoctorService } from '../doctor.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-dotors-filters',
  templateUrl: './dotors-filters.component.html',
  styleUrls: ['./dotors-filters.component.scss'],
})
export class DotorsFiltersComponent {
  filtersForm = new FormGroup({
    specialization: new FormControl('All', [Validators.required]),
    name: new FormControl(''),
  });

  isLoading$: Observable<boolean>;

  constructor(public doctorService: DoctorService) {
    this.isLoading$ = doctorService.isLoading$;
  }

  ngOnInit() {
    this.doctorService.filters$.subscribe((filters) => {
      this.filtersForm.setValue(filters);
    });
  }

  onSubmit() {
    if (this.filtersForm.valid) {
      this.doctorService.setFilters(this.filtersForm.value);
      this.doctorService.setIsLoading(true);
      console.log('Form submitted:', this.filtersForm.value);
    } else {
      console.log('Form is invalid. Please fill in all required fields.');
    }
  }
}

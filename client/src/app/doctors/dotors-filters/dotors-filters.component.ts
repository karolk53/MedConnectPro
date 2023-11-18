import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DoctorService } from '../doctor.service';

@Component({
  selector: 'app-dotors-filters',
  templateUrl: './dotors-filters.component.html',
  styleUrls: ['./dotors-filters.component.scss'],
})
export class DotorsFiltersComponent {
  filtersForm = new FormGroup({
    specialization: new FormControl('All', [Validators.required]),
    name: new FormControl('', Validators.required),
  });

  constructor(public doctorService: DoctorService) {}

  ngOnInit() {
    this.doctorService.filters$.subscribe((filters) => {
      this.filtersForm.setValue(filters);
    });
  }

  onSubmit() {
    this.doctorService.isLoading$.subscribe((isLoading) => {
      if (this.filtersForm.valid && isLoading !== true) {
        this.doctorService.setFilters(this.filtersForm.value);
        this.doctorService.setIsLoading(true);
        console.log('Form submitted:', this.filtersForm.value);
      } else {
        console.log(
          'Form is invalid or already loading. Please fill in all required fields.'
        );
      }
    });
  }
}

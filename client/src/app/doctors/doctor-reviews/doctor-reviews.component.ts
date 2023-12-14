import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { DoctorService } from '../doctor.service';
import { CommentModule } from 'src/app/shared/models/comment';

@Component({
  selector: 'app-doctor-reviews',
  templateUrl: './doctor-reviews.component.html',
  styleUrls: ['./doctor-reviews.component.scss']
})
export class DoctorReviewsComponent {
  @Input() doctorId = 1;
  isLoading = false;
  comment?: CommentModule;


  constructor(public doctorService: DoctorService) {}

  ngOnInit() {  
    this.doctorService.loadComments(this.doctorId);
  }

  updateComments() {
    const doctorId = 1; // Twój docelowy ID lekarza
  }

  validateValue(control: FormControl): ValidationErrors | null {
    const value = control.value;

    if (value % 1 === 0 && value >= 1 && value <= 5) {
      return null;
    } else {
      return { invalidValue: true };
    }
  }

  reviewForm = new FormGroup({
    value: new FormControl('', [
      Validators.required,
      this.validateValue.bind(this)
    ]),
    description: new FormControl('', Validators.required),
  });

  addNoteForDoctor() {
    this.isLoading = true;
    if (this.reviewForm.valid) {
      const note = {
        Description: this.reviewForm.value.description,
        Value: this.reviewForm.value.value
      };

      this.doctorService.addDoctorNote(this.doctorId, note).subscribe(
        () => {
          console.log('Note added successfully1');
          this.isLoading = false;
          this.doctorService.loadComments(this.doctorId);
        },
        (error) => {
          console.error('Error adding note', error);
          this.isLoading = false;
        }
      );
    } else {
      console.log('Form is not valid');
      this.isLoading = false;
    }
  }
}

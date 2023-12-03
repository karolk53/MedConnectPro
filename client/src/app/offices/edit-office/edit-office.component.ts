import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { OfficeService } from '../offices.service';

@Component({
  selector: 'app-edit-office',
  templateUrl: './edit-office.component.html',
  styleUrls: ['./edit-office.component.scss'],
})
export class EditOfficeComponent {
  isLoading = false;
  isSuccess = false;

  constructor(private officeService: OfficeService) {}

  OfficeForm = new FormGroup({
    Name: new FormControl('', [Validators.required]),
    Street: new FormControl('', Validators.required),
    BuildingNumber: new FormControl('', Validators.required),
    FlatNumber: new FormControl('', Validators.required),
    PostCode: new FormControl('', Validators.required),
    City: new FormControl('', Validators.required),
  });

  onSubmit() {
    this.isLoading = true;
    this.isSuccess = false;

    if (this.OfficeForm.valid) {
      const officeData = {
        Name: this.OfficeForm.value.Name,
        Street: this.OfficeForm.value.Street,
        BuildingNumber: this.OfficeForm.value.BuildingNumber,
        FlatNumber: this.OfficeForm.value.FlatNumber,
        PostCode: this.OfficeForm.value.PostCode,
        City: this.OfficeForm.value.City,
      };

      this.officeService.createOffice(officeData).subscribe(
        (response) => {
          console.log('Biuro utworzone pomyślnie', response);
          this.isSuccess = true;
        },
        (error) => {
          console.error('Błąd podczas tworzenia biura', error);
        }
      );
    }
    this.isLoading = false;
  }
}

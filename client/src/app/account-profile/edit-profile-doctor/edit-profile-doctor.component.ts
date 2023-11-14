import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from 'src/app/account/account.service';
import { User } from 'src/app/shared/models/user';

@Component({
  selector: 'app-edit-profile-doctor',
  templateUrl: './edit-profile-doctor.component.html',
  styleUrls: ['./edit-profile-doctor.component.scss'],
})
export class EditProfileDoctorComponent {
  userData: User | null = null;
  fileToUpload: File | null = null;

  public isSumbitingData = false;
  public isSumbitingDataError = false;
  public isSumbitingDataSucces = false;

  public isSumbitingPhoto = false;
  public isSumbitingPhotoError = false;
  public isSumbitingPhotoSucces = false;

  constructor(
    private accountService: AccountService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this.accountService.currentUser$.subscribe((user: User | null) => {
      this.userData = user;

      this.doctorDataForm.controls['email'].setValue(
        this.userData?.email || ''
      );
    });
  }

  doctorDataForm = new FormGroup({
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl('', Validators.required),
    phone: new FormControl('', Validators.required),
    gender: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
  });

  doctorPhotoForm = new FormGroup({
    profilePicture: new FormControl('', [Validators.required]),
  });

  onSubmitData() {
    this.isSumbitingData = true;
    this.isSumbitingDataError = false;
    this.isSumbitingDataSucces = false;

    let newData = {
      FirstName: this.doctorDataForm.value.firstName,
      LastName: this.doctorDataForm.value.lastName,
      Email: this.doctorDataForm.value.email,
      Phone: this.doctorDataForm.value.phone,
      Gender: this.doctorDataForm.value.gender,
    };

    console.log('submit1', newData);

    this.accountService.updateDoctorData(newData).subscribe(
      () => {
        console.log('Profile updated successfully!');
        this.isSumbitingData = false;
        this.isSumbitingDataSucces = true;
      },
      (error) => {
        console.error('Error updating profile:', error);
        this.isSumbitingData = false;
        this.isSumbitingDataError = true;
      }
    );
  }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
  }

  async onSubmitPhoto() {
    this.isSumbitingPhoto = true;
    this.isSumbitingPhotoError = false;
    this.isSumbitingPhotoSucces = false;

    this.accountService.addDoctorPhoto(this.fileToUpload).subscribe(
      () => {
        console.log('Image updated successfully!');
        this.isSumbitingPhotoSucces = true;
        this.isSumbitingPhoto = false;
      },
      (error) => {
        console.error('Error updating Image:', error);
        this.isSumbitingPhotoError = true;
        this.isSumbitingPhoto = false;
      }
    );
  }
}

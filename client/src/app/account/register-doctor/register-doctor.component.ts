import { Component } from '@angular/core';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { passwordMatchValidator } from '../password-validators';

@Component({
  selector: 'app-register-doctor',
  templateUrl: './register-doctor.component.html',
  styleUrls: ['./register-doctor.component.scss'],
})
export class RegisterDoctorComponent {
  returnUrl = '';
  complexPassword =
    '^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@#$!%^&+=])(?!.*\\s).{8,}$';

  constructor(
    private accountService: AccountService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  registerDoctorForm = new FormGroup(
    {
      email: new FormControl('', Validators.required),
      password: new FormControl('', [
        Validators.required,
        Validators.pattern(this.complexPassword),
      ]),
      confirmPassword: new FormControl('', [Validators.required]),
      pwz: new FormControl('', [
        Validators.required,
        Validators.minLength(7),
        Validators.maxLength(7),
      ]),
    },
    {
      validators: [passwordMatchValidator('password', 'confirmPassword')],
    }
  );

  onSubmit() {
    if (this.registerDoctorForm.valid) {
      this.accountService
        .registerNewDoctor(this.registerDoctorForm.value)
        .subscribe({
          next: () => this.router.navigateByUrl(this.returnUrl),
          error: (er) => {
            this.registerDoctorForm.setErrors({ registerFailed: true });
          },
        });
    }
  }
}

import { Component } from '@angular/core';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login-doctor',
  templateUrl: './login-doctor.component.html',
  styleUrls: ['./login-doctor.component.scss'],
})
export class LoginDoctorComponent {
  returnUrl = '';

  constructor(
    private accountService: AccountService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  loginDoctorForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
  });

  onSubmit() {
    AccountService;
    this.accountService.loginDoctor(this.loginDoctorForm.value).subscribe({
      next: () => this.router.navigateByUrl(this.returnUrl),
      error: () => this.loginDoctorForm.setErrors({ loginFailed: true }),
    });
  }
}

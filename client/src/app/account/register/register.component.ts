import { Component } from '@angular/core';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { passwordMatchValidator } from '../password-validators';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  returnUrl = '';
  complexPassword =
    '^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@#$!%^&+=])(?!.*\\s).{8,}$';

  constructor(
    private accountService: AccountService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  registerForm = new FormGroup(
    {
      email: new FormControl('', Validators.required),
      password: new FormControl('', [
        Validators.required,
        Validators.pattern(this.complexPassword),
      ]),
      confirmPassword: new FormControl('', [Validators.required]),
    },
    {
      validators: [passwordMatchValidator('password', 'confirmPassword')],
    }
  );

  onSubmit() {
    if (this.registerForm.valid) {
      this.accountService.register(this.registerForm.value).subscribe({
        next: () => this.router.navigateByUrl(this.returnUrl),
      });
    }
  }
}

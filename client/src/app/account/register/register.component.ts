import { Component } from '@angular/core';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  returnUrl = '';

  constructor(private accountService: AccountService, private router: Router, 
    private activatedRoute: ActivatedRoute) {
    }
  

  registerForm = new FormGroup({
    email: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
    replay_Password: new FormControl('',),
  });
  


  
  onSubmit() {
    AccountService
    this.accountService.register(this.registerForm.value).subscribe({
      next: () => this.router.navigateByUrl(this.returnUrl)
    });
  }
}

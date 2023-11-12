import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';

export const authPatientGuard = () => {
  const accountService = inject(AccountService);
  const router = inject(Router);

  return accountService.currentUser$.pipe(
    map((auth) => {
      console.log('auth', auth);
      if (auth?.role === 'Patient') return true;
      else {
        router.navigate(['/account/login'], {
          queryParams: { returnUrl: router.url },
        });
        return false;
      }
    })
  );
};

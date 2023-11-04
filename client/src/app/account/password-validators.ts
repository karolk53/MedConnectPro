import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function passwordMatchValidator(
  controlName: string,
  matchingControlName: string
): ValidatorFn {
  return (formGroup: AbstractControl): ValidationErrors | null => {
    const control = formGroup.get(controlName);
    const matchingControl = formGroup.get(matchingControlName);

    if (control && matchingControl && control.value !== matchingControl.value) {
      matchingControl.setErrors({ passwordMatch: true });
      return { passwordMatch: true };
    } else {
      matchingControl && matchingControl.setErrors(null);
      return null;
    }
  };
}

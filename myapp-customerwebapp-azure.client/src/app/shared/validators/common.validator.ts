import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function passwordMatchValidator(formGroup: AbstractControl): ValidationErrors | null {
  const password = formGroup.get('password')?.value;
  const confirmPassword = formGroup.get('confirmPassword')?.value;

  if (password !== confirmPassword) {
    formGroup.get('confirmPassword')?.setErrors({ passwordMismatch: true });
    return { passwordMismatch: true };
  }

  // Only clear error if it was previously set
  const errors = formGroup.get('confirmPassword')?.errors;
  if (errors && errors['passwordMismatch']) {
    delete errors['passwordMismatch'];
    if (Object.keys(errors).length === 0) {
      formGroup.get('confirmPassword')?.setErrors(null);
    } else {
      formGroup.get('confirmPassword')?.setErrors(errors);
    }
  }

  return null;
}
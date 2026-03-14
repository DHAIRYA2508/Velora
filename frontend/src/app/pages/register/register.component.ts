import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({ selector: 'app-register', templateUrl: './register.component.html' })
export class RegisterComponent {
  form: FormGroup;
  loading = false;
  error = '';
  showPw = false;

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) {
    if (auth.isLoggedIn) router.navigate(['/']);
    this.form = fb.group({
      username: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  err(f: string, e: string) { const c = this.form.get(f); return !!(c?.hasError(e) && c?.touched); }

  submit() {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.loading = true; this.error = '';
    this.auth.register(this.form.value).subscribe({
      next: () => { this.loading = false; this.router.navigate(['/menu']); },
      error: e => { this.loading = false; this.error = e.error?.message || 'Registration failed. Please try again.'; }
    });
  }
}

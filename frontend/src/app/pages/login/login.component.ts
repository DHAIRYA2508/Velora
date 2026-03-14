import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({ selector: 'app-login', templateUrl: './login.component.html' })
export class LoginComponent {
  form: FormGroup;
  loading = false;
  error = '';
  showPw = false;

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) {
    if (auth.isLoggedIn) router.navigate(['/']);
    this.form = fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  err(f: string, e: string) { const c = this.form.get(f); return !!(c?.hasError(e) && c?.touched); }

  submit() {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.loading = true; this.error = '';
    this.auth.login(this.form.value).subscribe({
      next: r => { this.loading = false; this.router.navigate([r.role === 'Admin' ? '/admin' : '/menu']); },
      error: e => { this.loading = false; this.error = e.error?.message || 'Invalid email or password.'; }
    });
  }
}

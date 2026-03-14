import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(private auth: AuthService, private router: Router) {}
  canActivate(route: ActivatedRouteSnapshot): boolean {
    if (!this.auth.isLoggedIn) { this.router.navigate(['/login']); return false; }
    const role = route.data['role'];
    if (role && this.auth.currentUser?.role !== role) { this.router.navigate(['/']); return false; }
    return true;
  }
}

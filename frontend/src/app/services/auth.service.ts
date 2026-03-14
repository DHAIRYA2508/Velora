import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Router } from '@angular/router';
import { AuthResponse, LoginDto, RegisterDto } from '../models/auth.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = '/api/auth';
  private userSubject = new BehaviorSubject<AuthResponse | null>(this.loadUser());
  currentUser$ = this.userSubject.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  private loadUser(): AuthResponse | null {
    try { const s = localStorage.getItem('velora_user'); return s ? JSON.parse(s) : null; }
    catch { return null; }
  }

  get currentUser(): AuthResponse | null { return this.userSubject.value; }
  get isLoggedIn(): boolean { return !!this.currentUser; }
  get isAdmin(): boolean { return this.currentUser?.role === 'Admin'; }
  get isCustomer(): boolean { return this.currentUser?.role === 'Customer'; }
  get token(): string | null { return this.currentUser?.token ?? null; }

  register(dto: RegisterDto): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/register`, dto).pipe(tap(r => this.save(r)));
  }

  login(dto: LoginDto): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, dto).pipe(tap(r => this.save(r)));
  }

  logout(): void {
    localStorage.removeItem('velora_user');
    this.userSubject.next(null);
    this.router.navigate(['/login']);
  }

  private save(r: AuthResponse): void {
    localStorage.setItem('velora_user', JSON.stringify(r));
    this.userSubject.next(r);
  }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MenuItem } from '../models/menu-item.model';

@Injectable({ providedIn: 'root' })
export class MenuService {
  private api = '/api/menu';
  constructor(private http: HttpClient) {}
  getAll(): Observable<MenuItem[]> { return this.http.get<MenuItem[]>(this.api); }
  getById(id: number): Observable<MenuItem> { return this.http.get<MenuItem>(`${this.api}/${id}`); }
  create(item: Omit<MenuItem,'id'>): Observable<MenuItem> { return this.http.post<MenuItem>(this.api, item); }
  update(id: number, item: MenuItem): Observable<MenuItem> { return this.http.put<MenuItem>(`${this.api}/${id}`, item); }
  delete(id: number): Observable<void> { return this.http.delete<void>(`${this.api}/${id}`); }
}

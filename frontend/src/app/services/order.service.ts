import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { MenuItem, CartItem } from '../models/menu-item.model';
import { Order } from '../models/order.model';

@Injectable({ providedIn: 'root' })
export class OrderService {
  private api = '/api/order';
  private cartSubject = new BehaviorSubject<CartItem[]>([]);
  cartItems$ = this.cartSubject.asObservable();
  constructor(private http: HttpClient) {}

  addToCart(item: MenuItem): void {
    const cur = this.cartSubject.getValue();
    const ex = cur.find(i => i.menuItem.id === item.id);
    if (ex) { ex.quantity++; this.cartSubject.next([...cur]); }
    else { this.cartSubject.next([...cur, { menuItem: item, quantity: 1 }]); }
  }
  remove(id: number): void { this.cartSubject.next(this.cartSubject.getValue().filter(i => i.menuItem.id !== id)); }
  updateQty(id: number, qty: number): void {
    if (qty <= 0) { this.remove(id); return; }
    const cur = this.cartSubject.getValue();
    const item = cur.find(i => i.menuItem.id === id);
    if (item) { item.quantity = qty; this.cartSubject.next([...cur]); }
  }
  clear(): void { this.cartSubject.next([]); }
  getTotal(): number { return this.cartSubject.getValue().reduce((s,i) => s + i.menuItem.price * i.quantity, 0); }
  getCount(): number { return this.cartSubject.getValue().reduce((s,i) => s + i.quantity, 0); }

  placeOrder(customerName: string): Observable<Order> {
    const items = this.cartSubject.getValue();
    return this.http.post<Order>(this.api, {
      customerName,
      orderItems: items.map(i => ({ menuItemId: i.menuItem.id, quantity: i.quantity, price: i.menuItem.price }))
    });
  }
  getOrders(): Observable<Order[]> { return this.http.get<Order[]>(this.api); }
  updateStatus(id: number, status: string): Observable<Order> { return this.http.put<Order>(`${this.api}/${id}/status`, { status }); }
}

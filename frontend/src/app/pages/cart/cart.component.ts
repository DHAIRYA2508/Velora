import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CartItem } from '../../models/menu-item.model';
import { OrderService } from '../../services/order.service';
import { AuthService } from '../../services/auth.service';

@Component({ selector: 'app-cart', templateUrl: './cart.component.html' })
export class CartComponent implements OnInit {
  items: CartItem[] = [];
  placing = false;
  placed = false;
  error = '';

  constructor(private orders: OrderService, public auth: AuthService, private router: Router) {}

  ngOnInit() { this.orders.cartItems$.subscribe(i => this.items = i); }

  get subtotal() { return this.orders.getTotal(); }
  get tax() { return this.subtotal * 0.1; }
  get total() { return this.subtotal + this.tax; }

  update(id: number, qty: number) { this.orders.updateQty(id, qty); }
  remove(id: number) { this.orders.remove(id); }

  placeOrder() {
    if (!this.items.length) return;
    this.placing = true; this.error = '';
    const name = this.auth.currentUser?.username || 'Guest';
    this.orders.placeOrder(name).subscribe({
      next: () => { this.orders.clear(); this.placing = false; this.placed = true; },
      error: (e) => { this.placing = false; this.error = e.error?.message || 'Failed to place order.'; }
    });
  }
}

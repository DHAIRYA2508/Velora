import { Component, OnInit, HostListener } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { OrderService } from '../../services/order.service';

@Component({ selector: 'app-navbar', templateUrl: './navbar.component.html' })
export class NavbarComponent implements OnInit {
  cartCount = 0;
  scrolled = false;
  menuOpen = false;

  constructor(public auth: AuthService, private orders: OrderService) {}

  ngOnInit() {
    this.orders.cartItems$.subscribe(() => this.cartCount = this.orders.getCount());
  }

  @HostListener('window:scroll')
  onScroll() { this.scrolled = window.scrollY > 40; }
}

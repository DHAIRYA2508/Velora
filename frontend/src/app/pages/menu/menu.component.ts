import { Component, OnInit } from '@angular/core';
import { MenuItem } from '../../models/menu-item.model';
import { MenuService } from '../../services/menu.service';
import { OrderService } from '../../services/order.service';
import { AuthService } from '../../services/auth.service';

@Component({ selector: 'app-menu', templateUrl: './menu.component.html' })
export class MenuComponent implements OnInit {
  items: MenuItem[] = [];
  filtered: MenuItem[] = [];
  categories: string[] = [];
  active = 'All';
  loading = true;
  added: { [id: number]: boolean } = {};

  constructor(private menu: MenuService, private orders: OrderService, public auth: AuthService) {}

  ngOnInit() {
    this.menu.getAll().subscribe(items => {
      this.items = items;
      this.filtered = items;
      this.categories = ['All', ...new Set(items.map(i => i.category))];
      this.loading = false;
    });
  }

  filter(cat: string) {
    this.active = cat;
    this.filtered = cat === 'All' ? this.items : this.items.filter(i => i.category === cat);
  }

  addToCart(item: MenuItem) {
    this.orders.addToCart(item);
    this.added[item.id] = true;
    setTimeout(() => delete this.added[item.id], 1500);
  }
}

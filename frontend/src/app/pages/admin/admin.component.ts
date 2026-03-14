import { Component, OnInit } from '@angular/core';
import { MenuItem } from '../../models/menu-item.model';
import { Order } from '../../models/order.model';
import { MenuService } from '../../services/menu.service';
import { OrderService } from '../../services/order.service';

@Component({ selector: 'app-admin', templateUrl: './admin.component.html' })
export class AdminComponent implements OnInit {
  tab: 'orders' | 'menu' = 'orders';
  orders: Order[] = [];
  menuItems: MenuItem[] = [];
  loading = true;
  showForm = false;
  editItem: MenuItem | null = null;
  deleteId: number | null = null;
  saving = false;
  formError = '';

  form: Partial<MenuItem> = { name: '', description: '', price: 0, imageUrl: '', category: '' };
  categories = ['Starters', 'Mains', 'Desserts', 'Beverages'];
  statuses = ['Pending', 'Preparing', 'Ready', 'Delivered', 'Cancelled'];

  constructor(private menu: MenuService, private orderService: OrderService) {}

  ngOnInit() { this.load(); }

  load() {
    this.loading = true;
    this.orderService.getOrders().subscribe(o => { this.orders = o; this.loading = false; });
    this.menu.getAll().subscribe(m => this.menuItems = m);
  }

  openAdd() { this.form = { name:'', description:'', price:0, imageUrl:'', category:'Starters' }; this.editItem = null; this.showForm = true; this.formError = ''; }
  openEdit(item: MenuItem) { this.form = { ...item }; this.editItem = item; this.showForm = true; this.formError = ''; }
  cancelForm() { this.showForm = false; }

  save() {
    if (!this.form.name || !this.form.category || !this.form.price) { this.formError = 'Please fill all required fields.'; return; }
    this.saving = true;
    const obs = this.editItem
      ? this.menu.update(this.editItem.id, this.form as MenuItem)
      : this.menu.create(this.form as Omit<MenuItem,'id'>);
    obs.subscribe({ next: () => { this.saving = false; this.showForm = false; this.menu.getAll().subscribe(m => this.menuItems = m); }, error: () => { this.saving = false; this.formError = 'Failed to save. Please try again.'; } });
  }

  confirmDelete(id: number) { this.deleteId = id; }
  cancelDelete() { this.deleteId = null; }
  doDelete() {
    if (!this.deleteId) return;
    this.menu.delete(this.deleteId).subscribe(() => { this.menuItems = this.menuItems.filter(i => i.id !== this.deleteId); this.deleteId = null; });
  }

  updateStatus(id: number, status: string) {
    this.orderService.updateStatus(id, status).subscribe(updated => {
      const i = this.orders.findIndex(o => o.id === id);
      if (i >= 0) this.orders[i] = updated;
    });
  }

  statusColor(s: string): string {
    return ({ Pending:'bg-amber-100 text-amber-700', Preparing:'bg-blue-100 text-blue-700', Ready:'bg-green-100 text-green-700', Delivered:'bg-gray-100 text-gray-600', Cancelled:'bg-red-100 text-red-600' } as any)[s] || 'bg-gray-100 text-gray-600';
  }

  get totalRevenue() { return this.orders.filter(o => o.status !== 'Cancelled').reduce((s,o) => s + (o.totalAmount||0), 0); }
  get pendingCount() { return this.orders.filter(o => o.status === 'Pending').length; }
}

import { Component, Input, Output, EventEmitter } from '@angular/core';
import { MenuItem } from '../../models/menu-item.model';

@Component({
  selector: 'app-menu-card',
  templateUrl: './menu-card.component.html',
  styleUrls: ['./menu-card.component.css']
})
export class MenuCardComponent {
  @Input() item!: MenuItem;
  @Output() addToCart = new EventEmitter<MenuItem>();

  onAddToCart(): void {
    this.addToCart.emit(this.item);
  }

  getImageUrl(): string {
    return this.item.imageUrl || 'https://images.unsplash.com/photo-1546069901-ba9599a7e63c?w=400&h=300&fit=crop';
  }
}

import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CartItem } from '../../models/menu-item.model';

@Component({
  selector: 'app-cart-item',
  templateUrl: './cart-item.component.html',
  styleUrls: ['./cart-item.component.css']
})
export class CartItemComponent {
  @Input() cartItem!: CartItem;
  @Output() quantityChanged = new EventEmitter<{ id: number; quantity: number }>();
  @Output() removed = new EventEmitter<number>();

  increaseQuantity(): void {
    this.quantityChanged.emit({ id: this.cartItem.menuItem.id, quantity: this.cartItem.quantity + 1 });
  }

  decreaseQuantity(): void {
    if (this.cartItem.quantity > 1) {
      this.quantityChanged.emit({ id: this.cartItem.menuItem.id, quantity: this.cartItem.quantity - 1 });
    } else {
      this.removed.emit(this.cartItem.menuItem.id);
    }
  }

  removeItem(): void {
    this.removed.emit(this.cartItem.menuItem.id);
  }

  getSubtotal(): number {
    return this.cartItem.menuItem.price * this.cartItem.quantity;
  }
}

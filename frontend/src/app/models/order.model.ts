export interface OrderItem { menuItemId: number; menuItemName?: string; quantity: number; price: number; }
export interface Order { id?: number; customerName: string; totalAmount: number; orderDate?: string; status?: string; orderItems: OrderItem[]; }
export interface CreateOrderDto { customerName: string; orderItems: { menuItemId: number; quantity: number; price: number }[]; }

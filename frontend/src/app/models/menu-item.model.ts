export interface MenuItem { id: number; name: string; description: string; price: number; imageUrl: string; category: string; }
export interface CartItem { menuItem: MenuItem; quantity: number; }

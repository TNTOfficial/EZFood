import { Product } from "../product/product.model";

export interface OrderItem {
    id: string;
    product: Product;
    quantity: number;
}
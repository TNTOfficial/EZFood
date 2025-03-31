import { Product } from "../product/product.model";

export interface CartItem {
    id: string;
    product: Product;
    quantity: number;
    quantityChanged: boolean
}
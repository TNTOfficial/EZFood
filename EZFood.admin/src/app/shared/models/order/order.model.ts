import { ShippingAddress } from "../shipping-address/shipping-address.model";
import { OrderItem } from "./order-item.model";

export interface Order {
    id: string;
    orderTotalAmount: number;
    orderBValue: number;
    shippingAddress?: ShippingAddress;
    orderDate: Date;
    name: string;
    contact: string;
    orderStatus: number;
}

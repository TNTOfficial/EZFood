import { AddressType } from "../enums/address-type";

export interface CreateShippingAddress {
  name: string;
  phoneNumber: string;
  pinCode: string;
  addressLine1: string;
  addressLine2: string;
  addressType: AddressType;
  isPrimary: boolean;
  cityId: number;
}

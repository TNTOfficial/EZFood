import { AddressType } from "../enums/address-type";

export interface ShippingAddress {
  id: string;
  name: string;
  phoneNumber: string;
  pinCode: string;
  addressLine1: string;
  addressLine2: string;
  addressType: AddressType;
  isPrimary: boolean;
  cityId: number;
  cityName: string;
  stateId: number;
  stateName: string;
  userId: string;
}

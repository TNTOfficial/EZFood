import { User } from "./user.model";

export interface UserDetail extends User {
  imageUrl:string;
  pinCode: string;
  address: string;
  createdAt: Date;

}

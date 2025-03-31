import { City } from "../city/city.model";
import { UserStatus } from "../enums/user-status";

export interface User {
  id: string;
  userCode: string;
  name: string;
  email: string;
  phoneNumber: string;
  userStatus: UserStatus;
  country: string;
  city?: City
}

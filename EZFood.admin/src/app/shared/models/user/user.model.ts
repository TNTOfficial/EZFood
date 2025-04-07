import { UserStatus } from "../enums/user-status";

export interface User {
  id: string;
  name: string;
  email: string;
  phoneNumber: string;
  userStatus: UserStatus;
}

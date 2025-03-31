export interface ChangePasswordRequest {
  userCode: string;
  currentPassword: string;
  newPassword: string;
}

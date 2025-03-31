export interface ResetPasswordRequest {
  userCode: string;
  token: string;
  newPassword: string;
}

export interface AuthenticatedUser {
  email: string;
  userCode: string;
  userId: string;
  name: string;
  roles: string[];
  isKycCompleted: string;
  hasAppliedForKyc: string;
  networkNodeId?: string;
  parentNodeId?: string;
  tokenExpiry: Date;
}

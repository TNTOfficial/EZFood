export interface DecodedToken {
  sub: string;
  jti: string;
  nameidentifier: string;
  userCode: string;
  userId: string;
  name: string;
  roles: string;
  isKycCompleted: string;
  hasAppliedForKyc: string;
  networkNodeId?: string;
  parentNodeId?: string;
  exp: number;
  iss: string;
}

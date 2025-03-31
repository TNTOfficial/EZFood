import { ApprovalStatus } from "../enums/approval-status";

export interface ApprovalRequest {
  id: string;
  userId: string;
  userCode: string;
  userName: string;
  requestType: string;
  requestedAt: Date;
  status: ApprovalStatus;
}

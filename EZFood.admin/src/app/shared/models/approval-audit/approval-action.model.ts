import { ApprovalStatus } from "../enums/approval-status";

export interface ApprovalAction {
  requestId: string;
  action: ApprovalStatus,
  rejectionReason?: string;
}

import { ApprovalStatus } from "../enums/approval-status";

export interface ApprovalAudit {
  id: string;
  entityType: string;
  oldStatus: ApprovalStatus;
  newStatus: ApprovalStatus;
  reviewedByName: string;
  comments?: string;
  actionTime: Date;
}

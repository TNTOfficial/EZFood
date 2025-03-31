import { ApprovalStatus } from "../../../shared/models/enums/approval-status";

export interface BankAccountChangeRequest {
  id: string;
  userId: string;
  accountNumber: string;
  bankName: string;
  branchName: string;
  ifscCode: string;
  panNumber: string;
  aadharNumber: string;
  status: ApprovalStatus;
  rejectionReason?: string;
  requestReason?:string;
  chequeImageUrl?:string;
  requestedAt: Date;
  reviewedAt?: Date;
  reviewedBy?: string;
}

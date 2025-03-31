import { ApprovalStatus } from "../enums/approval-status";
import { DocumentType } from "../enums/document-type";

export interface UserDocument {
  id: string;
  userId: string;
  documentType: DocumentType;
  fileName: string;
  filePath: string;
  status: ApprovalStatus;
  rejectionReason?: string;
  uploadedAt: Date;
  reviewedAt?: Date;
}

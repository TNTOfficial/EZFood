import { ApprovalStatus } from "../enums/approval-status";

export interface UserKyc {
  userId: string;
  userCode: string;
  name: string;
  email: string;
  phoneNumber: string;
  isPhotoUploaded: boolean;
  isPanCardUploaded: boolean;
  isAadharCardUploaded: boolean;
  isBankProofUploaded: boolean;
  photoStatus: ApprovalStatus;
  panCardStatus: ApprovalStatus;
  aadharCardStatus: ApprovalStatus;
  bankProofStatus: ApprovalStatus;
  rejectionReason?: string;
  isKycCompleted: boolean;
}

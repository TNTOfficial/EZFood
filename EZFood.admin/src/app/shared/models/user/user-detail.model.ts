import { RegistrationType } from "../enums/registration-type";
import { NetworkNodeForUserDetail } from "../network-node/network-node-for-user-detail.model";
import { BankAccount } from "./bank-account.model";
import { CoApplicant } from "./co-applicant.model";
import { User } from "./user.model";

export interface UserDetail extends User {
  imageUrl:string;
  pinCode: string;
  address: string;
  fatherName: string;
  husbandName: string;
  gender: string;
  dateOfBirth?: Date;
  martialStatus: string;
  profession: string;
  voterNo: string;
  gstNo: string;
  accptTerms: boolean;
  isNewPIBO: boolean;
  spouseWasPIBO: boolean;
  placementId: string;
  sponsorId: string;
  placementUserName?: string;
  sponsorUserName?: string;
  registrationType: RegistrationType;
  createdAt: Date;


  networkNode?: NetworkNodeForUserDetail;
  bankAccount?: BankAccount;
  coApplicant?: CoApplicant;
}

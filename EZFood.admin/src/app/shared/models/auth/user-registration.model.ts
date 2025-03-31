import { BankAccount } from "../user/bank-account.model";
import { CoApplicant } from "../user/co-applicant.model";
import { RegistrationType } from "../enums/registration-type";

export interface UserRegistration {
  name: string;
  fatherName?: string;
  husbandName?: string;
  dateOfBirth: Date;
  gender: string;
  address?: string;
  cityId: number;
  country: string;
  pinCode: string;
  phoneNumber: string;
  email: string;
  martialStatus?: string;
  accptTerms: boolean;
  profession?: string;
  sponsorId: string;
  placementId?: string;
  registrationType: RegistrationType;
  acceptTerms: boolean;
  bankAccount?: BankAccount;
  coApplicant?: CoApplicant;
}

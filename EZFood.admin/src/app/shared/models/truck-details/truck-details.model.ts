import { OnboardingStatus } from "../../enums/onboardingStatus";
import { FileOrigin } from "../enums/enums";

export interface TruckDetail {
  id: string;
  truckName: string;
  truckOwnerName: string;
  phoneNumber: string | undefined;
  businessEmail: string;
  address: string;
  isOtherCuisine: false;
  cuisineNote: "";
  cuisineTypes: string[];
  bussinessStartYear: number;
  businessDescription: string;
  ein: string;
  isBreakfast: boolean;
  isLunch: boolean;
  isDinner: boolean;
  minimumGuaranteeAmount: number | undefined;
  coi: string | null;
  w9: string | null;
  serveSafeCertificate: string | null;
  dchCertificate: string | null;
  imageList: string[];
  menuList: string[];
  onboardingStatus: OnboardingStatus;
}

export interface Step1 {
  truckName: string;
  truckOwnerName: string;
  phoneNumber: string | undefined;
  businessEmail: string;
  address: string;
}

export interface Step2 {
  isOtherCuisine: false;
  cuisineNote: "";
  cuisines: string[];
}
export interface Step3 {
  bussinessStartYear: number;
  businessDescription: string;
  ein: string;
  isBreakfast: boolean;
  isLunch: boolean;
  isDinner: boolean;
  minimumGuaranteeAmount: number | undefined;
  coi: string | null;
  w9: string | null;
  serveSafeCertificate: string | null;
  dchCertificate: string | null;
}

export interface Step4 {
  files: string[];
}

export interface Cuisine {
  id: string;
  name: string;
}

export interface PreviewImage {
 origin: FileOrigin;
 uri: string;
 fileName?: string;
 mimeType?: string;
}

export interface StepDocs {
  origin: FileOrigin;
  uri: string | null;
  file: File | null;
  isAvailable: boolean;
 }

export interface OnboardingData {
  step: OnboardingStatus;
  stepOne: Step1;
  stepTwo: Step2;
  stepThree: Step3;
  stepFour: Step4;
  stepFive: Step4;
}

export interface OnboardingResponse {
  result: boolean;
  data: OnboardingData;
}




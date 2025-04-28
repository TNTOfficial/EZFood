import { OnboardingStatus } from "../../enums/onboardingStatus";

export interface TruckDetail {
  id: string;
  truckName: string;
  truckOwnerName: string;
  phoneNumber: string | undefined;
  businessEmail: string;
  address: string;
  isOtherCuisine: false;
  cuisineNote: "";
  cuisines: string[];
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



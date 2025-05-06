import { OnboardingStatus } from "../enums/onboardingStatus";

export const getOnboardingStatus = (status: OnboardingStatus) => {
  switch(status) {
    case OnboardingStatus.Step1:
      return "Step 1";
    case OnboardingStatus.Step2:
      return "Step 2";
    case OnboardingStatus.Step3:
      return "Step 3";
    case OnboardingStatus.Step4:
      return "Step 4";
    case OnboardingStatus.Step5:
      return "Step 5";
    case OnboardingStatus.Pending:
      return "Confirm & submission";
    case OnboardingStatus.Submitted:
      return "Submitted for review";
    case OnboardingStatus.Objection:
      return "Referred back";
    case OnboardingStatus.Rejected:
      return "Rejected";
    default:
      return "Approved";
  }
}

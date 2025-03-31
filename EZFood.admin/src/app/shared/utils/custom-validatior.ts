import { AbstractControl, FormGroup } from "@angular/forms";

export class CustomValidator {
    // Validates URL
    static urlValidator(url: AbstractControl): any {
      if (url.pristine) {
        return null;
      }
      const URL_REGEXP = /^(http?|ftp|https?):\/\/([a-zA-Z0-9.-]+(:[a-zA-Z0-9.&%$-]+)*@)*((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])){3}|([a-zA-Z0-9-]+\.)*[a-zA-Z0-9-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(:[0-9]+)*(\/($|[a-zA-Z0-9.,?'\\+&%$#=~_-]+))*$/;
      url.markAsTouched();
      if (URL_REGEXP.test(url.value)) {
        return null;
      }
      return {
        invalidUrl: true
      };
    }
    // Validates passwords
    static matchPassword(group: any): any {
      const password = group.controls.password;
      const confirm = group.controls.confirm;
      if (password.pristine || confirm.pristine) {
        return null;
      }
      group.markAsTouched();
      if (password.value === confirm.value) {
        return null;
      }
      return {
        invalidPassword: true
      };
    }
    // Validates numbers
    static numberValidator(number: AbstractControl): any {
      if (number.pristine) {
        return null;
      }
      const NUMBER_REGEXP = /^-?[\d.]+(?:e-?\d+)?$/;
      number.markAsTouched();
      if (NUMBER_REGEXP.test(number.value)) {
        return null;
      }
      return {
        invalidNumber: true
      };
    }
   
    // Validates US phone numbers
    static phoneValidator(number: any): any {
      if (number.pristine) {
        return null;
      }
      const PHONE_REGEXP = /^(\([0-9]{3}\) |[0-9]{3}-)[0-9]{3}-[0-9]{4}$/;
      number.markAsTouched();
      if (PHONE_REGEXP.test(number.value)) {
        return null;
      }
      return {
        invalidNumber: true
      };
    }
  
  }
  
import { Pipe, PipeTransform } from "@angular/core";


@Pipe({
  name: "utcToIst"
})
export class UtcToIstPipe implements PipeTransform {
  transform(value: string | Date| undefined): Date {
    const utcDate = new Date(value!);
    return new Date(utcDate.getTime() - 4 * 60 * 60 * 1000)
  }
}

import { Inject, Pipe, PipeTransform, inject } from "@angular/core";
import { DomSanitizer, SafeResourceUrl } from "@angular/platform-browser";


@Pipe({
  name:"safeUrl"
})
export class SafeUrlPipe implements PipeTransform {
  sanitizer = inject( DomSanitizer);

  transform(url: string): SafeResourceUrl {
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);
  }
}

import { CommonModule } from "@angular/common";
import { Component, Input } from "@angular/core";
import { AlertCircle, LucideAngularModule } from "lucide-angular";


@Component({
  selector: "app-error",
  standalone: true,
  imports: [CommonModule, LucideAngularModule],
  template: `
    <div class="p-3 bg-red-100 text-red-800 rounded-lg flex items-start">
      <lucide-icon [name]="AlertCircle" class="text-red-800 mr-2 mt-1" size="18"></lucide-icon>
      <span>{{ message }}</span>
    </div>
  `,
  styles: ``
})
export class ErrorComponent {
  AlertCircle = AlertCircle;
  @Input() message: string = ''
}

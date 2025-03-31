import { Component,  Injectable, signal } from '@angular/core';
import {
  LucideAngularModule,
  AlignJustify,
  ChevronDown,
  Heart,
  MapPin,
  PhoneCall,
  Search,
  ShoppingCart,
  Sun,
  User,
  X,
} from 'lucide-angular';
import { provideTablerIcons, TablerIconComponent } from 'angular-tabler-icons';
import {
  IconAccessPoint,
  IconApps,
  IconBrandWhatsapp,
  IconHeartFilled,
  IconLanguage,
  IconMoonFilled,
  IconRazorElectric,
} from 'angular-tabler-icons/icons';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'frontend-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
  imports: [LucideAngularModule, TablerIconComponent, RouterLink],
  providers: [
    provideTablerIcons({
      IconRazorElectric,
      IconHeartFilled,
      IconAccessPoint,
      IconBrandWhatsapp,
      IconMoonFilled,
      IconLanguage,
      IconApps,
    }),
  ],
})
@Injectable({
  providedIn: 'root',
})
export class FrontendHeaderComponent {
  PhoneCall = PhoneCall;
  Search = Search;
  Sun = Sun;
  Heart = Heart;
  User = User;
  ShoppingCart = ShoppingCart;
  AlignJustify = AlignJustify;
  ChevronDown = ChevronDown;
  X = X;
  MapPin = MapPin;

  isDarkMode = signal(false);

  themeService = this;

  toggleDarkMode() {
    this.isDarkMode.update((value) => !value);
    document.documentElement.classList.toggle('dark', this.isDarkMode());
  }
}

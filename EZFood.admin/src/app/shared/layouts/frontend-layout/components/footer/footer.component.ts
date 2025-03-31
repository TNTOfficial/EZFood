import { Component } from '@angular/core';
import AOS from 'aos';

import {
  LucideAngularModule,
  MapPin,
  Mail,
} from 'lucide-angular';
import { provideTablerIcons, TablerIconComponent } from 'angular-tabler-icons';
import {
  IconBrandGooglePlay,
  IconBrandApple,
  IconBrandWhatsapp,
  IconBrandInstagram,
  IconBrandFacebook,
  IconBrandTwitter,
  IconBrandLinkedin
} from 'angular-tabler-icons/icons';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'frontend-footer',
  standalone: true,
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.css',

  imports: [
    LucideAngularModule,
    TablerIconComponent,
    RouterLink,
  ],
  providers: [
    provideTablerIcons({
      IconBrandGooglePlay,
      IconBrandApple,
      IconBrandWhatsapp,
      IconBrandInstagram,
      IconBrandFacebook,
      IconBrandTwitter,
      IconBrandLinkedin
      
    }),
  ],
})
export class FooterComponent {
  // Swiper Slider init
  title = 'swiper-elements';
  // Lucide Icons
  MapPin = MapPin
  Mail = Mail
}
AOS.init({
  once: true,
});

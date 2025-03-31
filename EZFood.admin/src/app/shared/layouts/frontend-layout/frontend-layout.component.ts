import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FrontendHeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';

@Component({
  selector: 'app-frontend-layout',
  standalone: true,
  imports: [RouterOutlet, FrontendHeaderComponent, FooterComponent],
  templateUrl: './frontend-layout.component.html',
  styleUrl: './frontend-layout.component.css'
})
export class FrontendLayoutComponent {

}

import { Component  } from '@angular/core';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { RouterOutlet } from '@angular/router';
import { AdminHeaderComponent } from './components/admin-header/admin-header.component';
import { ToastContainerComponent } from '../../components/toast-container/toast-container.component';

@Component({
  selector: 'app-admin-layout',
  standalone: true,
  imports: [SidebarComponent, AdminHeaderComponent,RouterOutlet, ToastContainerComponent],
  templateUrl: './admin-layout.component.html',
})
export class AdminLayoutComponent {
  sideClose:boolean = false;
  handleSideClose = (sideClose: any): void => {
    this.sideClose = !this.sideClose
  }
}

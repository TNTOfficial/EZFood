import { Injectable, signal } from "@angular/core";


export interface ToastMessage {
  id: number;
  type: 'success' | 'error' | 'info' | 'warning';
  title: string;
  message: string;
  duration: number;
}
@Injectable({
  providedIn: "root"
})
export class ToastService {
  private lastId = 0;
  toasts = signal<ToastMessage[]>([]);


  success(title: string, message: string, duration: number = 5000) {
    this.addToast({
      id: ++this.lastId,
      type: 'success',
      title,
      message,
      duration
    });
  }

  error(title: string, message: string, duration: number = 5000): void {
    this.addToast({
      id: ++this.lastId,
      type: 'error',
      title,
      message,
      duration
    });
  }

  info(title: string, message: string, duration: number = 5000): void {
    this.addToast({
      id: ++this.lastId,
      type: 'info',
      title,
      message,
      duration
    });
  }

  warning(title: string, message: string, duration: number = 5000): void {
    this.addToast({
      id: ++this.lastId,
      type: 'warning',
      title,
      message,
      duration
    });
  }



  private addToast(toast: ToastMessage): void {
    this.toasts.update(toasts => [...toasts, toast]);

    // Auto-remove after duration
    setTimeout(() => {
      this.removeToast(toast.id);
    }, toast.duration);
  }

  public removeToast(id: number): void {
    this.toasts.update(toasts => toasts.filter(t => t.id !== id));
  }
}

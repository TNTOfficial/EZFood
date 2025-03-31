import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
 providedIn: 'root'
})
export class ImageService {
    getImageUrl = (relPath: string) => `${environment.assetsUrl}${relPath}`;
}

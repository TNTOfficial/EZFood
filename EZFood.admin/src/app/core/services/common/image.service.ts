import { Injectable } from '@angular/core'; 
import { environment } from '../../../../environments/environment';

@Injectable({
 providedIn: 'root'
})
export class ImageService {
    getImageUrl = (relPath: string | undefined) => `${environment.assetsUrl}${relPath}`;
}
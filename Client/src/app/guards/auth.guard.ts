import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AlertifyService } from '../services/Alertify.service';
import { UserServicesService } from '../services/UserServices.service';



@Injectable({
  providedIn: 'root'
})
export class GuardsGuard implements CanActivate {
  constructor(private user: UserServicesService, private router: Router, private alertify: AlertifyService) { }
  canActivate(): boolean {
    if (this.user.loggedIn()) {
      return true;
    }
    this.alertify.error('You sall not pass!!!');
    this.router.navigate(['/home']);
    return false;
  }

}

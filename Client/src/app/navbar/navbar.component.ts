import { Component, OnInit } from '@angular/core';
import { UserServicesService } from '../services/UserServices.service';
import { AlertifyService } from '../services/Alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  model: any = {};

  constructor(public userService: UserServicesService, private alertifyService: AlertifyService, private router: Router) { }

  ngOnInit() {
  }

  login() {
    this.userService.login(this.model)
      .subscribe(res => {
        const user = res;
        this.alertifyService.success("Logged in Successfully!");
        this.router.navigate(['expenses']);
      }, error => {
        this.alertifyService.error('Unauthorized!');
      });
  }
  loggedIn() {
    return this.userService.loggedIn();
  }
  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.userService.decodedToken = null;
    this.model = {};
    this.alertifyService.warning('Logged Out');
    this.router.navigate(['']);
  }
}

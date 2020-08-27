import { Component, OnInit } from '@angular/core';
import { UserServicesService } from './services/UserServices.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  jwtHelper = new JwtHelperService();

  constructor(private userServices: UserServicesService) { }
  ngOnInit() {
    const token = localStorage.getItem('token');
    if (token) {
      this.userServices.decodedToken = this.jwtHelper.decodeToken(token);
    }
  }
}

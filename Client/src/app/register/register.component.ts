import { Component, OnInit } from '@angular/core';
import { User } from '../Models/User';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserServicesService } from '../services/UserServices.service';
import { AlertifyService } from '../services/Alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  user: User;
  registerForm: FormGroup;

  constructor(
    private userService: UserServicesService,
    private alertifyService: AlertifyService,
    private fb: FormBuilder,
    private router: Router
  ) { }

  ngOnInit() {
    this.createdRegisterForm();
    this.phoneMask();
  }
  createdRegisterForm() {
    this.registerForm = this.fb.group({
      FirstName: ['', [Validators.required, Validators.maxLength(12), Validators.minLength(3)]],
      LastName: ['', [Validators.required, Validators.maxLength(12), Validators.minLength(3)]],
      Phone: ['', Validators.required],
      Email: ['', Validators.required],
      Username: ['', [Validators.required, Validators.maxLength(12), Validators.minLength(3)]],
      Password: ['', [Validators.required, Validators.maxLength(8), Validators.minLength(8)]]
    });
  }

  register() {

    if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
      this.userService.register(this.user).subscribe(() => {
        this.alertifyService.success('Resgistration successful');
      }, err => {
        this.alertifyService.error(err);
      }, () => {
        this.userService.login(this.user).subscribe(() => {
          this.router.navigate(['expenses']);
        });
      });
    }
  }
  phoneMask() {
    const phone = document.querySelector('.phone');
    phone.addEventListener("input", (e) => {
      const target = e.target as HTMLInputElement;
      target.value = target.value
        .replace(/\D/g, "")
        .replace(/(\d{0})(\d)/, "$1($2")
        .replace(/(\d{2})(\d)/, "$1) $2")
        .replace(/(\d{5})(\d)/, "$1-$2")
        .replace(/(-\d{4})\d+?$/, "$1")
    });
  }
}

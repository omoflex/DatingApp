import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyjsService } from '../_services/alertifyjs.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};
  constructor(public authService: AuthService, private alertify: AlertifyjsService) { }
  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged in successfully');
    },
    error => {
      this.alertify.error(error);
    }
    );
  }

  ngOnInit() {
  }

  loggedIn(){
   return this.authService.LoggedIn();
  }

  logOut(){
    localStorage.removeItem('token');
    this.alertify.message('Logged out');
  }

}

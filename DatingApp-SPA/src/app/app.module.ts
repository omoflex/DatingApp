import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import { NavComponent } from './nav/nav.component';
import { Login2Component } from './Login2/Login2.component';
import { AuthService } from './_services/auth.service';
import { TestService } from './_services/test.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';


@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      Login2Component,
      HomeComponent,
      RegisterComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule
   ],
   providers: [
      AuthService,
      TestService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }

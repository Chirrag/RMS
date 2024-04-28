import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private jwtHelper: JwtHelperService) { }
  isLoggedIn: boolean = false;

  tokenCheck() {
    try {
      const token = JSON.parse(sessionStorage.getItem('_token'));

      if (this.jwtHelper.isTokenExpired(token.token)) {
        this.isLoggedIn = false;
      } else {
        this.isLoggedIn = true;
      }
    } catch (error) {
      sessionStorage.clear();
      this.isLoggedIn = false;
    }
  }



  isAuthenticated() {
    this.tokenCheck();
    return this.isLoggedIn;
  }
}

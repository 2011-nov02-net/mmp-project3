import { Component, OnInit, Input } from '@angular/core';
import {OktaAuthService} from '@okta/okta-angular';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

isAuthenticated: Boolean=false;

  constructor(public oktaAuth: OktaAuthService, public router: Router) {
    this.oktaAuth.$authenticationState.subscribe(
      (isAuthenticated: boolean)  => this.isAuthenticated = isAuthenticated
    );
   }

   async ngOnInit() {
    // Get the authentication state for immediate use
    this.isAuthenticated =  await this.oktaAuth.isAuthenticated();
 };
  login() {
    this.oktaAuth.signInWithRedirect();
  }
  async logout() {
    // Terminates the session with Okta and removes current tokens.
    await this.oktaAuth.signOut();
  }

}

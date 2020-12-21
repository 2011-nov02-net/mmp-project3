import { Component, OnInit, Input } from '@angular/core';
import {OktaAuthService} from '@okta/okta-angular';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

@Input() isAuthenticated: Boolean=false;

  constructor(public oktaAuth: OktaAuthService) {
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

}

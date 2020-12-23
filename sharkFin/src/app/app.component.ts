import { Component, OnInit, OnChanges } from '@angular/core';
import { Router } from '@angular/router';
import { OktaAuthService } from '@okta/okta-angular';
import {User} from './Models/user';
import {Portfolio} from './Models/portfolio';
import {Stock} from './Models/stock';
import {UserService} from './user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'sharkFin';
  isAuthenticated: boolean = false;
  user: User = {id: 0, name: 'Test', email: 'Test@test.com', portfolios: []};
  portfolios: Portfolio[] = [{id: 0, name: 'Portfolio1', funds: 500  }, {id: 1, name: 'Portfolio2', funds: 500 },{id: 2, name: 'Portfolio3', funds: 500 },{id: 3, name: 'Portfolio4', funds: 500 }]

  constructor(public oktaAuth: OktaAuthService, public router: Router, public userService: UserService) {
    // Subscribe to authentication state changes
    this.oktaAuth.$authenticationState.subscribe(
      (isAuthenticated: boolean)  => this.isAuthenticated = isAuthenticated
    );
  };
  

  async ngOnInit() {
     // Get the authentication state for immediate use
     this.user = {id: 0, name: 'Test', email: 'Test@test.com', portfolios: []};
     this.isAuthenticated =  await this.oktaAuth.isAuthenticated();
     const userClaim = await this.oktaAuth.getUser();
     //this.userService.getUserByEmail(userClaim.email!).subscribe(user => this.user = user);
  };  
    

    
  login() {
    this.oktaAuth.signInWithRedirect();
  }

  async logout() {
    // Terminates the session with Okta and removes current tokens.
    this.oktaAuth.tokenManager.clear();
    await this.oktaAuth.signOut();
  }
}

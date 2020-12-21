import { Component, OnInit, OnChanges } from '@angular/core';
import { Router } from '@angular/router';
import { OktaAuthService } from '@okta/okta-angular';
import {User} from './Models/user';
import {UserService} from './user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'sharkFin';
  isAuthenticated: boolean = false;
  user: User = {id: 0, name: '', email: ''};

  constructor(public oktaAuth: OktaAuthService, public router: Router, public userService: UserService) {
    // Subscribe to authentication state changes
    this.oktaAuth.$authenticationState.subscribe(
      (isAuthenticated: boolean)  => this.isAuthenticated = isAuthenticated
    );
  };
  

  async ngOnInit() {
     // Get the authentication state for immediate use
     this.isAuthenticated =  await this.oktaAuth.isAuthenticated();
     const userClaim = await this.oktaAuth.getUser();
     this.userService.getUserByEmail(userClaim.email!).subscribe(user => this.user = user);
  };  
    

    
  login() {
    this.oktaAuth.signInWithRedirect();
  }

  async logout() {
    // Terminates the session with Okta and removes current tokens.
    await this.oktaAuth.signOut();
  }
}

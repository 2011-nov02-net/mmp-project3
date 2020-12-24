import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OktaCallbackComponent, OktaAuthGuard} from '@okta/okta-angular';
import {HomeComponent} from './home/home.component';
import {UserComponent} from './user/user.component';
import { PortfolioComponent } from './portfolio/portfolio.component';


@NgModule({
  imports: [
 
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }

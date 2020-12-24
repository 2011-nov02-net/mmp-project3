import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OktaCallbackComponent, OktaAuthGuard} from '@okta/okta-angular';
import {HomeComponent} from './home/home.component';
import {UserComponent} from './user/user.component';
import { PortfolioComponent } from './portfolio/portfolio.component';

const routes: Routes = [

  {
    path: 'login/callback',
    component: OktaCallbackComponent
  },{
    path: 'user/:id',
    canActivate: [OktaAuthGuard],
    component: UserComponent
  },{
    path: 'portfolio/:id',
    canActivate: [OktaAuthGuard],
    component: PortfolioComponent
  }, 
  {
    path: '',
  component: HomeComponent
  },
 
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OktaCallbackComponent, OktaAuthGuard} from '@okta/okta-angular';
import {HomeComponent} from './home/home.component';
import {UserComponent} from './user/user.component';
import { PortfolioComponent } from './portfolio/portfolio.component';
import { LeaderboardComponent } from './leaderboard/leaderboard.component';

const routes: Routes = [

 {
    path: 'user/:id',
    canActivate: [OktaAuthGuard],
    component: UserComponent
  },{
    path: 'portfolio/:id',
    canActivate: [OktaAuthGuard],
    component: PortfolioComponent
  }, {
    path: 'login/callback',
    component: OktaCallbackComponent
  },
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'leaderboard',
    component: LeaderboardComponent
  }
 
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }

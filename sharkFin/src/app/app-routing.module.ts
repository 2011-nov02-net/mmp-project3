import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OktaCallbackComponent, OktaAuthGuard} from '@okta/okta-angular';
import {HomeComponent} from './home/home.component';
import {UserComponent} from './user/user.component';
const routes: Routes = [
  {
    path: '',
  component: HomeComponent
  },
  {
    path: 'login/callback',
    component: OktaCallbackComponent
  },{
    path: 'user',
    canActivate: [OktaAuthGuard],
    component: UserComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }

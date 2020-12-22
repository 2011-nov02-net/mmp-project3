import { Component, OnInit, Input } from '@angular/core';
import { User } from '../Models/user';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { UserService } from '../user.service';
import { Portfolio } from '../Models/portfolio';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  @Input() user!: User;
  @Input() portfolios! : Portfolio[];
  
  constructor( private route: ActivatedRoute,
    private userService: UserService,
    private location: Location) { }

  ngOnInit(): void {
    this.user = history.state.user;
    this. portfolios = history.state.portfolios;
    // this.getUser();
  }

  getUser(): void {
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.userService.getUser(id).
    subscribe(user => this.user = user);
  }

}

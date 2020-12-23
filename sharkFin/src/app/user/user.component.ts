import { Component, OnInit, Input } from '@angular/core';
import { User } from '../Models/user';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { UserService } from '../user.service';
import { Portfolio } from '../Models/portfolio';
import {PortfolioService} from '../portfolio.service';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  @Input() user: User = {firstName: '', lastName: '', id: 0, email: ''};

  
  constructor( private route: ActivatedRoute,
    private userService: UserService,
    private location: Location,
    private portfolioService: PortfolioService) { }

  ngOnInit(): void {
     this.getUser();
  }

  getUser(): void {
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.userService.getUser(id).
    subscribe(user => this.user = user, () => console.log(this.user));
  
  }

  newPortfolio(name: string){
    this.portfolioService.createPortfolio(name, this.user.id);
  }

}

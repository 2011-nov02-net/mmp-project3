import { Component, OnInit } from '@angular/core';
import { PortfolioService } from '../portfolio.service';
import { UserService } from '../user.service';
import {User} from '../Models/user';
import { StockService } from '../stock.service';
import {Portfolio} from '../Models/portfolio';

@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.css']
})
export class LeaderboardComponent implements OnInit {
  users: User[] = [];
  sorted: boolean = false;

  constructor(private userService: UserService, private portfolioService: PortfolioService, private stockService: StockService) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe(u => {
      this.users = u;
      console.log(this.users);
      this.userTrimAndSort();
    })

  }

  userTrimAndSort() : void {
    let newUsers: User[] = [];
    for(let i =0; i < this.users.length; i ++){
      if(this.users[i].portfolios!.length > 0){
       
        newUsers.push(this.users[i])
      }
    }
    console.log(newUsers);
    newUsers.sort(function (a, b) {
      return b.portfolios![0].funds - a.portfolios![0].funds;
    })
    console.log(newUsers);

    this.users = newUsers;
    this.sorted = true;

  }
  // portTally(portfolio: Portfolio)  {
  //   let totalVal: number = portfolio.funds;
  //     if(portfolio.assets){
  //       for(let i = 0; i < portfolio.assets.length; i++){
  //         this.stockService.getStockPriceApi(portfolio.assets[i].stock.symbol).subscribe((p => {
  //           portfolio.assets![i].price = p.c;
  //           totalVal += (portfolio.assets![i].price! * portfolio.assets![i].quantity)
            
  //         }));        
  //       }
  //     }
      
  //   }

}

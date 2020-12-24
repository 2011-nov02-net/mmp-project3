import { Component, OnInit, Input } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Stock } from '../Models/stock';
import { StockService} from '../stock.service';
import {
  debounceTime, distinctUntilChanged, switchMap
} from 'rxjs/operators';
import { StockSearch } from '../stockSearch';
import { User } from '../Models/user';
import { OktaAuthService } from '@okta/okta-angular';
import { UserService } from '../user.service';
import { PortfolioService } from '../portfolio.service';
import { Asset } from '../Models/asset';
import { AssetService } from '../asset.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  searchRes: StockSearch = {name:'', ticker: '', logo: '', price: 0, exchange: '', industry: ''};
  @Input() user!: User;
  isAuthenticated: boolean = false;
 
  constructor(private stockService: StockService, public oktaAuth: OktaAuthService, public userService: UserService, public portfolioService: PortfolioService, public assetService: AssetService) { 
    this.oktaAuth.$authenticationState.subscribe(
      (isAuthenticated: boolean)  => this.isAuthenticated = isAuthenticated
    );
  }

  async ngOnInit() {
    // Get the authentication state for immediate use
    this.isAuthenticated = await this.oktaAuth.isAuthenticated();
    if (this.isAuthenticated) {
      const userClaims = await this.oktaAuth.getUser();
      console.log(userClaims);
      if(userClaims.email){
       this.userService.getUserByEmail(userClaims.email).subscribe(user => this.user = user);
      }
    
    }
    
 }; 

  search($event: any, term: string) {
    $event.preventDefault();
    term = term.toUpperCase();
    this.stockService.getStockInfoApi(term).subscribe(data => {this.searchRes.name = data.name, this.searchRes.ticker = data.ticker, this.searchRes.exchange = data.exchange, this.searchRes.logo = data.logo, this.searchRes.industry = data.finnhubIndustry},
      err => console.error(err));
    this.stockService.getStockPriceApi(term).subscribe(data => {this.searchRes.price = data.c});    
  }

  addToPortfolio($event: any, symbol: string, quant: string) {
    $event.preventDefault();
    let stock : Stock = {symbol: symbol, market: this.searchRes.exchange, name: this.searchRes.name, logo: this.searchRes.logo, id:0}
    let portfolioId = this.user.portfolios![0].id;
    let asset: Asset = {stock: stock, quantity: parseInt(quant)}
    //console.log(asset);
    this.assetService.addToPortfolio(asset, portfolioId, this.searchRes.price);

  }
}

// name: data.name,
//       exchange: data.exchange,
//       ticker: data.ticker,
//       logo: data.logo,
//       industry: data.finnhubIndustry
import { Component, OnInit, Input } from '@angular/core';
import { User } from '../Models/user';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { UserService } from '../user.service';
import { Portfolio } from '../Models/portfolio';
import {PortfolioService} from '../portfolio.service';
import {OktaAuthService} from '@okta/okta-angular';
import { StockService } from '../stock.service';
import { AssetService } from '../asset.service';
import {Asset} from '../Models/asset';
import {Stock} from '../Models/stock';


@Component({
  selector: 'app-portfolio',
  templateUrl: './portfolio.component.html',
  styleUrls: ['./portfolio.component.css']
})
export class PortfolioComponent implements OnInit {

  @Input() user: User = {firstName: '', lastName: '', id: 0, email: '', currentPort: 0};
  portfolio!: Portfolio;
  isAuthenticated: boolean = false;
  totalVal: number = 0;

  
  constructor( private route: ActivatedRoute,
    private userService: UserService,
    private location: Location,
    public portfolioService: PortfolioService,
    public oktaAuth: OktaAuthService,
    public stockService: StockService,
    public assetService: AssetService) { }

  async ngOnInit() {

    this.isAuthenticated = await this.oktaAuth.isAuthenticated();
    if (this.isAuthenticated) {
      const userClaims = await this.oktaAuth.getUser();
      console.log(userClaims);
      if(userClaims.email){
       this.userService.getUserByEmail(userClaims.email).subscribe(user => this.user = user);
      }
     this.getPortfolio();
  }
}

ngAfterViewInit() {
  this.portTally();
}


  getPortfolio() : void {
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.portfolioService.getPortfolio(id).subscribe(p => this.portfolio = p);
  }

  portTally()  {
  this.totalVal += this.portfolio.funds;
    if(this.portfolio.assets){
      for(let i = 0; i < this.portfolio.assets.length; i++){
        this.stockService.getStockPriceApi(this.portfolio.assets[i].stock.symbol).subscribe((p => {
          this.portfolio.assets![i].price = p.c;
          this.addValue(this.portfolio.assets![i].price! * this.portfolio.assets![i].quantity)
        }));        
      }
    }
    
  }

  addValue(num: number) {
    this.totalVal += num;    
  }

  sell(portId: number, assetId: number, price: number, quantity: number, stock: Stock){
    let asset : Asset = {id: assetId, quantity: quantity, price: price, stock: stock}
    this.assetService.removeFromPortfolio(portId, asset, quantity)
    location.reload();  
  }
    

  }

  


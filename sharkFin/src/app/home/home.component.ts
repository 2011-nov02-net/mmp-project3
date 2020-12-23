import { Component, OnInit } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Stock } from '../Models/stock';
import { StockService} from '../stock.service';
import {
  debounceTime, distinctUntilChanged, switchMap
} from 'rxjs/operators';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  searchRes!: {};
  searchPrice!: {};
  constructor(private stockService: StockService) { }

  ngOnInit(): void {

  }

  search(term: string) {

    term = term.toUpperCase();
    this.stockService.getStockInfoApi(term).subscribe(data => {this.searchRes = data},
      err => console.error(err));
    this.stockService.getStockPriceApi(term).subscribe(data => {this.searchPrice = data});    
  }

  addToPortfolio(symbol: string, quant: number) {
    console.log("Will buy " + quant + " shares of " + symbol)
  }

}

// name: data.name,
//       exchange: data.exchange,
//       ticker: data.ticker,
//       logo: data.logo,
//       industry: data.finnhubIndustry
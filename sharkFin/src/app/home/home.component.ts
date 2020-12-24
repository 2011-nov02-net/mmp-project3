import { Component, OnInit } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Stock } from '../Models/stock';
import { StockService} from '../stock.service';
import {
  debounceTime, distinctUntilChanged, switchMap
} from 'rxjs/operators';
import { StockSearch } from '../stockSearch';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  searchRes: StockSearch = {name:'', ticker: '', logo: '', price: 0, exchange: '', industry: ''};
 
  constructor(private stockService: StockService) { }

  ngOnInit(): void {

  }

  search($event: any, term: string) {
    $event.preventDefault();
    term = term.toUpperCase();
    this.stockService.getStockInfoApi(term).subscribe(data => {this.searchRes.name = data.name, this.searchRes.ticker = data.ticker, this.searchRes.exchange = data.finnhubIndustry, this.searchRes.logo = data.logo},
      err => console.error(err));
    this.stockService.getStockPriceApi(term).subscribe(data => {this.searchRes.price = data.c});    
  }

  addToPortfolio(symbol: string, quant: string) {
    // let numQuant = parseInt(quant);
    // console.log("Will buy " + numQuant + " shares of " + symbol)
    // this.searchRes.
  }

}

// name: data.name,
//       exchange: data.exchange,
//       ticker: data.ticker,
//       logo: data.logo,
//       industry: data.finnhubIndustry
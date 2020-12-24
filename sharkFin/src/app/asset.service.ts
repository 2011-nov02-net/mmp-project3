import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { MessageService } from './message.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import {User} from './Models/user';
import { environment } from 'src/environments/environment';
import { Asset } from './Models/asset';
import { Portfolio } from './Models/portfolio';
import { Stock } from './Models/stock';
@Injectable({
  providedIn: 'root'
})
export class AssetService {
  private baseUrl = `${environment.baseUrl}/api/trades`;
  httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
  };

  constructor(private http: HttpClient,
    private messageService: MessageService) { }

    addToPortfolio(asset: Asset, id: number, price: number) {
     
      // this.http.get<Stock>(`${environment.baseUrl}/symbol/${asset.stock.symbol}`).subscribe(s => asset.stock.id = s.id);
      //   console.log(asset);
        this.http.post(`${environment.baseUrl}/api/portfolios/${id}/assets`,{id: 0, quantity: asset.quantity, stock:{id: 0, symbol: asset.stock.symbol, market: asset.stock.market, name: asset.stock.name, logo: asset.stock.logo }}).subscribe();

        this.http.post(`${environment.baseUrl}/api/portfolios/${id}/trades`, {id: 0, stock: {id: 0, symbol: asset.stock.symbol, market: asset.stock.market, name: asset.stock.name, logo: asset.stock.logo }, quantity: asset.quantity, price: price}).subscribe(); 
        let cost = asset.quantity*price;
        this.http.put(`${environment.baseUrl}/api/portfolios/${id}/funds`, {funds: cost}).subscribe();
      
        
      ;
      
    }

    removeFromPortfolio(id: number, asset: Asset, price: number){
      this.http.delete(`${environment.baseUrl}/api/Assets/${asset.id}`).subscribe();

      this.http.post(`${environment.baseUrl}/api/portfolios/${id}/trades`, {id: 0, stock: {id: asset.id, symbol: '', market: asset.stock.market, name: asset.stock.name, logo: asset.stock.logo }, quantity: -asset.quantity, price: -price}).subscribe(); 
      let cost = -asset.quantity*price;
      this.http.put(`${environment.baseUrl}/api/portfolios/${id}/funds`, {funds: cost}).subscribe();
      console.log(asset);
    
      
    
    }

   




  

    
}
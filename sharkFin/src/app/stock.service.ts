import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { MessageService } from './message.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import {User} from './Models/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StockService {
  private baseUrl = `${environment.baseUrl}/api/users`;
  private apiUrl = "https://finnhub.io/api/v1"
  private finKey = 'bv99a5748v6ujthqgip0'
  httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
  };

  constructor(private http: HttpClient,
    private messageService: MessageService) { }

    getStockInfoApi(symbol: string): Observable<any>{
      return this.http.get<any>(this.apiUrl + '/stock/profile2?symbol=' + symbol + '&token=' + this.finKey);
    }

    getStockPriceApi(symbol: string) : Observable<any> {
      return this.http.get<any>(this.apiUrl + '/quote?symbol=' + symbol + '&token=' + this.finKey)
    }





  

    private handleError<T>(operation = 'operation', result?: T) {
      return (error: any): Observable<T> => {
  
        // TODO: send the error to remote logging infrastructure
        console.error(error); // log to console instead
  
        // TODO: better job of transforming error for user consumption
        this.log(`${operation} failed: ${error.message}`);
  
        // Let the app keep running by returning an empty result.
        return of(result as T);
      };
    }
    private log(message: string) {
      this.messageService.add(`HeroService: ${message}`);
    }
}
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { MessageService } from './message.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import {User} from './Models/user';
import { environment } from 'src/environments/environment';
import { OktaAuthService } from '@okta/okta-angular';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = `${environment.baseUrl}/api/users`;
  httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
  };

  constructor(private http: HttpClient,
    private messageService: MessageService, private oktaAuth: OktaAuthService) { }

    getUser(id: number): Observable<User>{
      return this.http.get<User>(`${this.baseUrl}/${id}`)
      .pipe(
         tap(_ => this.log(`fetched User id=${id}`)), 
         catchError(this.handleError<User>(`getUser id=${id}`))
       );
    }

    getUserByEmail(email: string): Observable<User>{
      return this.http.get<User>(this.baseUrl + '/email/' +email).pipe(
        tap(_ => this.log(`fetched User email=${email}`)), 
        catchError(this.handleError<User>(`getUser email=${email}`))
      );
    }

    getUsers(): Observable<User[]> {
      return this.http.get<User[]>(this.baseUrl);
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

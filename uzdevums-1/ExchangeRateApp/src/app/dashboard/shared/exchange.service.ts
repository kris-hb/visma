import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError, map, retry } from 'rxjs/operators';

import { Rate } from './rate.model';
import { CurrencyObject } from './currency-object.model';

@Injectable({
  providedIn: 'root'
})
export class ExchangeService {

  private EXCHANGE_RATE_API = 'https://api.exchangeratesapi.io/latest';

  constructor(private http: HttpClient) { }

  getLatestRates(): Observable<Rate[]> {
    return this.http.get<CurrencyObject>(this.EXCHANGE_RATE_API)
    .pipe(
      map((response: CurrencyObject) =>
        Object.entries(response.rates).map(([currency, value]) => ({currency, value}) as Rate)
      ),
      retry(3),
      catchError(this.handleError)
    );
  }

  private handleError(): Observable<never> {
    return throwError('Something bad happened.Please try again later.');
  }
}

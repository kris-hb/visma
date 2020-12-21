import { ChangeDetectorRef, Component, OnInit } from '@angular/core';

import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { ExchangeService } from './shared/exchange.service';
import { Rate } from './shared/rate.model';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  rates: Observable<Rate[]>;
  isLoading: boolean;
  errorMessage: string;
  constructor(private exchangeService: ExchangeService, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {

  }

  onButtonClick(): void {
    this.isLoading = true;
    this.errorMessage = null;
    this.rates = this.exchangeService.getLatestRates().pipe(
      catchError((error: string) => {
        this.errorMessage = error;
        this.isLoading = false;
        return of([]);
      }
    ));
  }

  onLoaded(isLoading: boolean): void {
    this.isLoading = isLoading;
    this.cdr.detectChanges();
  }

}

import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { DebugElement } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatTableModule} from '@angular/material/table';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';

import { of } from 'rxjs';

import { CurrencyTableComponent } from './currency-exchange-table/currency-table.component';
import { DashboardComponent } from './dashboard.component';
import { ExchangeService } from './shared/exchange.service';

describe('DashboardComponent', () => {
  let component: DashboardComponent;
  let fixture: ComponentFixture<DashboardComponent>;
  let exchangeService: ExchangeService;
  let exchangeServiceStub: any;
  let progressBar: DebugElement;
  let getLatestRatesSpy: any;


  beforeEach(async(() => {
    exchangeServiceStub = jasmine.createSpyObj('ExchangeService', ['getLatestRates']);
    getLatestRatesSpy = exchangeServiceStub.getLatestRates.and.returnValue(of([{currency: 'USD', value: 111}]));

    TestBed.configureTestingModule({
      declarations: [ DashboardComponent, CurrencyTableComponent],
      imports: [
        MatButtonModule,
        MatTableModule,
        MatPaginatorModule,
        MatProgressBarModule,
        BrowserAnimationsModule
      ],
      providers: [{ provide: ExchangeService, useValue: exchangeServiceStub }]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardComponent);
    exchangeService = TestBed.inject(ExchangeService);
    component = fixture.componentInstance;
    fixture.detectChanges();
    progressBar = fixture.debugElement.query(By.css('mat-progress-bar'));
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should be no progress bar and table in the DOM after component is created', () => {
    expect(component.rates).toBeFalsy();
    expect(progressBar).toBeFalsy();
  });

  it('should be displayed progress bar and exchangeService should be called after button is clicked', () => {
    component.onButtonClick();
    expect(getLatestRatesSpy.calls.any()).toBe(true, 'getLatestRate called');
    expect(component.isLoading).toBeTruthy();
  });

});

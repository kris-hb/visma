import { TestBed } from '@angular/core/testing';
import {HttpClientModule} from '@angular/common/http';
import {HttpTestingController, HttpClientTestingModule} from '@angular/common/http/testing';

import { ExchangeService } from './exchange.service';

describe('ExchangeService', () => {
  let service: ExchangeService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
        imports: [HttpClientModule, HttpClientTestingModule],
        providers: [ExchangeService]
    });
    service = TestBed.inject(ExchangeService);
    httpMock = TestBed.inject(HttpTestingController);
    });

  it('should be able to retrieve posts from the API via GET', () => {
        const dummyData: any[] = [{
            rates: [{usd: 111}],
        }];

        service.getLatestRates().subscribe(result => {
            expect(result.length).toBe(1);
            expect(result).toEqual(dummyData);
        });
        const request = httpMock.expectOne( 'https://api.exchangeratesapi.io/latest');
        expect(request.request.method).toBe('GET');
        request.flush(dummyData);
    });

});

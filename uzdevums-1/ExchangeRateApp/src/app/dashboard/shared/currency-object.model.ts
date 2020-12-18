import { Currency } from './currency.model';

export interface CurrencyObject {
    rates: Currency;
    base: string;
    date: string;
}

import { ChangeDetectorRef, Component, EventEmitter, Input, OnChanges, OnInit, Output, ViewChild } from '@angular/core';

import {MatTableDataSource} from '@angular/material/table';
import {MatPaginator} from '@angular/material/paginator';

import { Rate } from '../shared/rate.model';

@Component({
  selector: 'app-currency-table',
  templateUrl: './currency-table.component.html',
  styleUrls: ['./currency-table.component.scss']
})
export class CurrencyTableComponent implements OnInit, OnChanges  {

  displayedColumns: string[] = ['currency', 'value'];
  dataSource: MatTableDataSource<Rate>;
  @Input() data: Rate[];
  @Output() loading = new EventEmitter<boolean>();
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource<Rate>();
  }
  ngOnChanges(): void {
    if (this.data) {
      this.dataSource.data = this.data;
      this.cdr.detectChanges();
      this.dataSource.paginator = this.paginator;
      this.loading.emit(false);
    }
  }
}

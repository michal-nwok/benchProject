import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {Stock} from "../models/stock";

const header: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

@Injectable({
  providedIn: 'root'
})
export class StockService {
  httpOptions = {
    headers: header
  };
  constructor(private http: HttpClient) { }

  getStocks(): Observable<Stock[]> {
    return this.http.get<Stock[]>('https://localhost:7031/v1/stock', this.httpOptions);
  }

  getStocksBetweenDates(dateFrom: string, dateTo: string) : Observable<Stock[]>
  {
    return this.http.get<Stock[]>('https://localhost:7031/v1/stock/'+encodeURIComponent(dateFrom)+'/'+encodeURIComponent(dateTo), this.httpOptions);
  }
}

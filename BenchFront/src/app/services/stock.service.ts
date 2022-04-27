import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {Stock} from "../models/stock";
import {environment} from "../../environments/environment";

export interface Response {
  stocks: Stock[];
}

const header: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

@Injectable({
  providedIn: 'root'
})
export class StockService {
  httpOptions = {
    headers: header
  };
  constructor(private http: HttpClient) { }

  getStocksBetweenDates(dateFrom: string, dateTo: string) : Observable<Response>
  {
    let apiString = environment.apiUrl + 'stocksByDates?dateFrom=' + encodeURIComponent(dateFrom)+'&dateTo='+encodeURIComponent(dateTo);
    return this.http.get<Response>(apiString , this.httpOptions);
  }
}

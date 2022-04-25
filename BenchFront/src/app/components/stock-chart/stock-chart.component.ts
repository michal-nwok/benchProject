import { Component, OnInit } from '@angular/core';
import * as Highcharts from 'highcharts';
import {StockService} from "../../services/stock.service";
@Component({
  selector: 'app-stock-chart',
  templateUrl: './stock-chart.component.html',
  styleUrls: ['./stock-chart.component.css']
})
export class StockChartComponent implements OnInit {

  constructor(private stocksService: StockService) { }

  ngOnInit(): void {
    this.stocksService.getStocks().subscribe(result => {
      console.log(result);
    })
    console.log()
  }

}

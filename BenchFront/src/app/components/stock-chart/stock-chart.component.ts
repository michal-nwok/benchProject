import { Component, OnInit } from '@angular/core';
import * as Highcharts from 'highcharts/highstock';
import {StockService} from "../../services/stock.service";
@Component({
  selector: 'app-stock-chart',
  templateUrl: './stock-chart.component.html',
  styleUrls: ['./stock-chart.component.css']
})
export class StockChartComponent implements OnInit {
  Highcharts: typeof Highcharts = Highcharts;
  chartData: Array<number[]> = [[], []];
  constructorType: string = 'stockChart';
  chartOptions: Highcharts.Options = {
    title: {
      text: 'Bench Stocks'
    },
    series: [{
      data: this.chartData,
      type: 'line',
    }],
  };
  constructor(private stocksService: StockService) { }

  ngOnInit(): void {
    this.stocksService.getStocks().subscribe(result => {
      this.chartData = result.map(stock => [new Date(stock.date).getTime(), stock.closePrice])
      this.updateChart(this.chartData);
    })
  }

  updateChart(newData: Array<number[]>) {
    this.chartOptions = {
      series: [{
        data: newData,
        type: 'line'
      }]
    };
  }

}

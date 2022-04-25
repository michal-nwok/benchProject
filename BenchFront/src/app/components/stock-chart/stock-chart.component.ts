import { Component, OnInit } from '@angular/core';
import * as Highcharts from 'highcharts/highstock';
import {StockService} from "../../services/stock.service";
import {MatDatepickerInputEvent} from "@angular/material/datepicker";
import {FormControl, FormGroup} from "@angular/forms";
import {DatePipe} from "@angular/common";
@Component({
  selector: 'app-stock-chart',
  templateUrl: './stock-chart.component.html',
  styleUrls: ['./stock-chart.component.css']
})
export class StockChartComponent implements OnInit {
  Highcharts: typeof Highcharts = Highcharts;
  chartData?: Array<number[]>;
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

  range = new FormGroup({
    start: new FormControl(),
    end: new FormControl(),
  });

  constructor(private stocksService: StockService, public datepipe: DatePipe) { }

  ngOnInit(): void {
  }

  updateChart(newData: Array<number[]>) {
    this.chartOptions = {
      series: [{
        data: newData,
        type: 'line'
      }]
    };
  }

  generateChart(event: MatDatepickerInputEvent<any>) {
    if(this.range.value.start == null || this.range.value.end == null || this.range.value.start > this.range.value.end)
    {
      return;
    }

    let dateFrom = this.datepipe.transform(this.range.value.start, 'yyyy-MM-dd HH:mm:ss')
    let dateTo = this.datepipe.transform(this.range.value.end, 'yyyy-MM-dd HH:mm:ss')

    this.stocksService.getStocksBetweenDates(dateFrom!, dateTo!).subscribe(result => {
      this.chartData = result.map(stock => [new Date(stock.date).getTime(), stock.closePrice])
      this.updateChart(this.chartData);
    })
  }

}

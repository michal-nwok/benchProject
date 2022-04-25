import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {StockChartComponent} from "./components/stock-chart/stock-chart.component";

const routes: Routes = [
  {path: '', component: StockChartComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

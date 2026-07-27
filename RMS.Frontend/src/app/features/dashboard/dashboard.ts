import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { NgApexchartsModule } from 'ng-apexcharts';
import { PageHeaderService } from '../../core/services/page-header';
/*
import {
  NgApexchartsModule,
  ApexChart,
  ApexLegend,
  ApexDataLabels,
  ApexStroke,
  ApexNonAxisChartSeries
} from 'ng-apexcharts';

export type ChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  labels: string[];
  legend: ApexLegend;
  dataLabels: ApexDataLabels;
  stroke: ApexStroke;
  colors: string[];
};
*/
@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    MatCardModule,
    NgApexchartsModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard {

chartOptions = {
 // public chartOptions: ChartOptions = {
    series: [82, 18],
    chart: {
      type: 'donut',
      height: 280
    },
    labels: [
      'Used',
      'Available'
    ],

    legend: {
      position: 'bottom'
    },

    dataLabels: {
      enabled: false
    },

    stroke: {
      width: 0
    },

    colors: [
      '#2563eb',
      '#e5e7eb'
    ]
  };
   constructor(private pageHeaderService: PageHeaderService) {}

  ngOnInit() {
  
    this.pageHeaderService.setTitle('Dashboard')
  }
}

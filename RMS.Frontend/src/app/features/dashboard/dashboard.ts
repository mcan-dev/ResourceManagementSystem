import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { NgApexchartsModule } from 'ng-apexcharts';

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
    series: [82, 18],

    chart: {
      type: 'donut' as const,
      height: 280
    },

    labels: [
      'Used',
      'Available'
    ]
  };

}
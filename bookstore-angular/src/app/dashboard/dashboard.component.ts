import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Statistics, StatisticsService, Response } from '../core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})

export class DashboardComponent implements OnInit {

  public Stats: Observable<Statistics>;

  constructor(private statisticsService: StatisticsService) {}

  ngOnInit(): void {
    this.loadStats()
  }

  loadStats(){
    return this.statisticsService.getStatistics().subscribe((res: Response<Observable<Statistics>>) => {
        this.Stats = res.result;
      },
      (err) => {
        console.log(err);
      });
  }
}

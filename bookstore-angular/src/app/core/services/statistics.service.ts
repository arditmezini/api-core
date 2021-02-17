import { Injectable } from '@angular/core';
import { AppSettings } from '../common';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root',
})
export class StatisticsService {
  constructor(private api: ApiService) {}

  getStatistics(){
      return this.api.get(`${AppSettings.ApiV1}/statistics/dashboard`);
  }

}
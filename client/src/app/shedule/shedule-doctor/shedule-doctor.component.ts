import { Component, OnInit } from '@angular/core';
import { ScheduleService } from '../shedule.service';

@Component({
  selector: 'app-shedule-doctor',
  templateUrl: './shedule-doctor.component.html',
  styleUrls: ['./shedule-doctor.component.scss'],
})
export class SheduleDoctorComponent implements OnInit {
  data: any;

  constructor(private scheduleService: ScheduleService) {}

  ngOnInit() {
    this.scheduleService.scheduleInfo$.subscribe((scheduleInfo) => {
      console.log('scheduleInfo', scheduleInfo);
      if (scheduleInfo) {
        this.data = this.sortScheduleByWeekDay(scheduleInfo);
      } else {
        this.data = []; // Lub inna obsługa, gdy scheduleInfo ma wartość null
      }
    });
  }

  sortScheduleByWeekDay(scheduleInfo: any[]): any[] {
    if (!scheduleInfo || scheduleInfo.length === 0) {
      return [];
    }

    const daysOrder = [
      'Monday',
      'Tuesday',
      'Wednesday',
      'Thursday',
      'Friday',
      'Saturday',
      'Sunday',
    ];

    // Sortowanie danych po dniu tygodnia
    scheduleInfo.sort((a, b) => {
      return daysOrder.indexOf(a.weekDay) - daysOrder.indexOf(b.weekDay);
    });

    return scheduleInfo;
  }
}

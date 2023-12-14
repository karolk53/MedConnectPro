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
      this.data = scheduleInfo;
    });
  }
}

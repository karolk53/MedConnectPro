import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ScheduleService } from '../shedule.service';

@Component({
  selector: 'app-shedule-edit',
  templateUrl: './shedule-edit.component.html',
  styleUrls: ['./shedule-edit.component.scss'],
})
export class SheduleEditComponent {
  isClassActive: string[] = [];
  isLoading = false;
  isSuccess = false;

  dayMappings: any = {
    Mon: 'Monday',
    Tue: 'Tuesday',
    Wed: 'Wednesday',
    Thu: 'Thursday',
    Fri: 'Friday',
    Sat: 'Saturday',
    Sun: 'Sunday',
  };

  constructor(private scheduleService: ScheduleService) {}

  toggleClass(day: string) {
    const index = this.isClassActive.indexOf(day);

    if (index === -1) {
      this.isClassActive.push(day);
    } else {
      this.isClassActive.splice(index, 1);
    }
  }

  scheduleForm = new FormGroup({
    start: new FormControl('', [Validators.required]),
    end: new FormControl('', Validators.required),
    length: new FormControl('', Validators.required),
  });

  onSubmit() {
    this.isLoading = true;
    this.isSuccess = false;
    if (this.scheduleForm.valid && this.isClassActive.length > 0) {
      const scheduleData = {
        StartHour: this.scheduleForm.value.start,
        EndHour: this.scheduleForm.value.end,
        VisitTime: this.scheduleForm.value.length,
      };
      console.log('scheduleData', scheduleData);

      const token = 'twój-token'; // Pobierz token z autoryzacji

      this.isClassActive.forEach((shortDay) => {
        const fullDay = this.dayMappings[shortDay];

        if (fullDay) {
          const scheduleForDay = {
            ...scheduleData,
            WeekDay: fullDay,
          };

          this.scheduleService.addSchedule(scheduleForDay).subscribe(
            (response) => {
              console.log(
                `Grafik dodany pomyślnie dla dnia ${fullDay}`,
                response
              );
              this.isSuccess = true;
            },
            (error) => {
              console.error(
                `Błąd podczas dodawania grafiku dla dnia ${fullDay}`,
                error
              );
            }
          );
        }
      });
      this.isLoading = false;
    }
  }
}

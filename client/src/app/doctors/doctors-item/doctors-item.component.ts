import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { DoctorService } from '../doctor.service';
import { Shedule, doctorInfo } from 'src/app/shared/models/doctorInfo';
import { ɵparseCookieValue } from '@angular/common';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-doctors-item',
  templateUrl: './doctors-item.component.html',
  styleUrls: ['./doctors-item.component.scss'],
})
export class DoctorsItemComponent {
  isLoading = true;
  doctor?: doctorInfo;
  doctorSchedule: any[] = [];
  startDate = "";
  endDate = "";
  weeksToAddOrSubtract = 1;
  weekDays: any[] = [];
  plannedAppointments: any[] = [];
  mergedSchedule: any[] = [];


  constructor(
    private activatedRoute: ActivatedRoute,
    private bcService: BreadcrumbService,
    private doctorService: DoctorService,
    private http: HttpClient,
    private router: Router
  ) {
    this.bcService.set('@Doctor', '');
  }

  ngOnInit(): void {
    this.loadDoctor();
    console.log('doctor', this.doctor);
  }

  loadDoctor() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id)
      this.doctorService.getDoctor(+id).subscribe({
        next: (doctor) => {
          this.doctor = doctor;
          this.bcService.set('@Doctor', doctor.email);
          this.isLoading = false;
          console.log('doctor', doctor);
          this.doctorSchedule = doctor.office.shedules;
          this.loadSchedule();

        },
        error: (error) => console.log(error),
      });
  }

  loadSchedule(){
    this.sortAndFillScheduleByWeekDay();
    this.getCurrentWeekDates(this.weeksToAddOrSubtract);
    this.getPlannedAppointments();
  }

  changeWeek(plus:boolean){
    plus ? this.weeksToAddOrSubtract++ : this.weeksToAddOrSubtract--;
    this.loadSchedule();
    this.sortAndFillScheduleByWeekDay();
    this.getCurrentWeekDates(this.weeksToAddOrSubtract);
    this.getPlannedAppointments();
  }

  sortAndFillScheduleByWeekDay() {
    const daysOfWeek = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];

    // Wypełnianie danymi z doctorSchedule i dodawanie dni tygodnia z pustymi danymi
    const sortedAndFilledSchedule = daysOfWeek.map(day => {
      const scheduleItem = this.doctorSchedule.find(item => item.weekDay === day);
      return scheduleItem || { weekDay: day, hours: [], visitTime: 0 };
    });

    this.doctorSchedule = sortedAndFilledSchedule;
    console.log('sorted', this.doctorSchedule);

  }

  getCurrentWeekDates(weeksToAddOrSubtract: number = 0){
    const today = new Date();
    const currentDay = today.getDay();
    const diff = today.getDate() - currentDay + (currentDay === 0 ? -6 : 1); // Poniedziałek bieżącego tygodnia
    const startOfWeek = new Date(today.setDate(diff));
    
    const startDate = new Date(startOfWeek);
    startDate.setDate(startOfWeek.getDate() + 7 * weeksToAddOrSubtract);

    const endDate = new Date(startDate);
    endDate.setDate(startDate.getDate() + 6);
    
    this.weekDays = this.generateWeekDays(startDate, endDate);
    this.startDate = this.formatDate(startDate);
    this.endDate = this.formatDate(endDate);

    console.log('date', {
      startDate: this.formatDate(startDate),
      endDate: this.formatDate(endDate),
      week: this.weekDays
    })
    

  }

  private formatDate(date: Date): string {
    const day = date.getDate();
    const month = date.getMonth() + 1; // Dodajemy 1, bo miesiące są liczone od 0 do 11
    const year = date.getFullYear();

    // Dodajemy zero przed jednocyfrowymi dniami i miesiącami
    const formattedDay = day < 10 ? `0${day}` : `${day}`;
    const formattedMonth = month < 10 ? `0${month}` : `${month}`;

    return `${formattedDay}-${formattedMonth}-${year}`;
  }

  private generateWeekDays(startDate: Date, endDate: Date): { day: string, date: string }[] {
    const weekDays = [];
    const currentDate = new Date(startDate);

    while (currentDate <= endDate) {
      const dayOfWeek = currentDate.toLocaleDateString('en-US', { weekday: 'long' });
      const formattedDate = this.formatDate(currentDate);

      weekDays.push({ day: dayOfWeek, date: formattedDate });

      currentDate.setDate(currentDate.getDate() + 1);
    }

    return weekDays;
  }

  getPlannedAppointments() {
      const baseUrl = 'https://localhost:5001/api/doctors';
      const url = `${baseUrl}/planned/${this.doctor?.id}?startDate=${this.startDate}&endDate=${this.endDate}`;
      
      this.http.get<any[]>(url).subscribe(
        (data) => {
          this.plannedAppointments = data;
          this.mergeSchedules(this.doctorSchedule, this.plannedAppointments);
        },
        (error) => {
          console.error('Error fetching planned appointments:', error);
        }
      );
  }

  mergeSchedules(currentSchedule: any[], plannedAppointments: any[]) {
    const mergedSchedule = currentSchedule.map(day => {
      const plannedAppointmentsForDay = plannedAppointments.filter(appointment => appointment.dayOfWeek === day.weekDay);
  
      const updatedHours = day.hours.map((hour: any)=> {
        const isHourFree = plannedAppointmentsForDay.some(appointment => appointment.hour === hour);
  
        return {
          hour,
          free: !isHourFree,
        };
      });
  
      return {
        ...day,
        hours: updatedHours,
      };
    });
    this.mergedSchedule = mergedSchedule;
    console.log('mergedSchedule', mergedSchedule)
  }

  navigateToVisitAdd(hour: string, dateIndex: number) {
    const queryParams = {
      hour: hour,
      doctorId: this.doctor?.id,
      date: this.weekDays[dateIndex].date
    };
  
    this.router.navigate(['/visit/add'], { queryParams: queryParams });
  }
}

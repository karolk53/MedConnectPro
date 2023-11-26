import { Component, OnInit } from '@angular/core';
import { OfficeService } from '../offices.service';

@Component({
  selector: 'app-show-office',
  templateUrl: './show-office.component.html',
  styleUrls: ['./show-office.component.scss'],
})
export class ShowOfficeComponent implements OnInit {
  data: any;

  constructor(private officeService: OfficeService) {}

  ngOnInit() {
    this.officeService.officeInfo$.subscribe((office) => {
      this.data = office;
      console.log('office', office);
    });
  }
}

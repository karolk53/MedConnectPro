import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-doctor-list-item',
  templateUrl: './doctor-list-item.component.html',
  styleUrls: ['./doctor-list-item.component.scss'],
})
export class DoctorListItemComponent {
  @Input() firstName = '';
  @Input() photoUrl = '';
  @Input() id = 0;
  @Input() email = '';
  @Input() lastName = '';
  @Input() rating = 0;
  @Input() ratingCount = 0;
}

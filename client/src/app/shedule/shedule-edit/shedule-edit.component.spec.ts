import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SheduleEditComponent } from './shedule-edit.component';

describe('SheduleEditComponent', () => {
  let component: SheduleEditComponent;
  let fixture: ComponentFixture<SheduleEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SheduleEditComponent]
    });
    fixture = TestBed.createComponent(SheduleEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

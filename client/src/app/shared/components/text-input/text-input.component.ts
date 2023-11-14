import { Component, EventEmitter, Input, Output, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss'],
})
export class TextInputComponent implements ControlValueAccessor {
  @Input() type = 'text';
  @Input() label = '';
  @Input() value = '';
  @Output() fileInputChanged: EventEmitter<FileList> =
    new EventEmitter<FileList>();

  onFileInputChange(event: any): void {
    this.fileInputChanged.emit(event.target.files);
  }

  constructor(@Self() public controlDir: NgControl) {
    this.controlDir.valueAccessor = this;
  }

  writeValue(obj: any): void {}
  registerOnChange(fn: any): void {}
  registerOnTouched(fn: any): void {}

  get control(): FormControl {
    return this.controlDir.control as FormControl;
  }
}

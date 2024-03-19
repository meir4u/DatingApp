import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-inputs',
  templateUrl: './text-inputs.component.html',
  styleUrls: ['./text-inputs.component.scss']
})
export class TextInputsComponent implements ControlValueAccessor {
  @Input() label = '';
  @Input() type = 'text';

  constructor(
    @Self() public ngControl: NgControl   
  ) {
    this.ngControl.valueAccessor = this;
  }
    writeValue(obj: any): void {
    }
    registerOnChange(fn: any): void {
    }
    registerOnTouched(fn: any): void {
    }
    //setDisabledState?(isDisabled: boolean): void {
    //    throw new Error('Method not implemented.');
  //}
  get control(): FormControl {
    return this.ngControl.control as FormControl;
  }
}

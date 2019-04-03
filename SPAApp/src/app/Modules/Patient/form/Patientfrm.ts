import { Component, Inject, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChange, SimpleChanges } from '@angular/core';
import { IPatient } from '../../../model/patient';


@Component({
  selector: 'Patient-Form',
  templateUrl: './Patientfrm.html'
})
export class PatientFormComponent implements OnInit, OnChanges {
  @Input() objPatient: IPatient;
  @Output() notify: EventEmitter<IPatient> = new EventEmitter<IPatient>();
  constructor(

  ) {
    
  }
  ngOnInit() {
    // load any lookup type data for dropdown
  }
  ngOnChanges(changes: SimpleChanges) {
    if (changes.objPatient && !changes.objPatient.isFirstChange()) {
      // exteranl API call or more preprocessing...
      this.objPatient = changes.objPatient.currentValue;
    }

    //for (let propName in changes) {
    //    let change = changes['objPatient'];
    //    this.objPatient = change.currentValue;
    //    //if (change.isFirstChange()) {
    //    //    this.objPatient = change.currentValue;
    //    //} else {
    //    //    console.log('no change in data')
    //    //}
    //}
  }

  onSave() {
    this.notify.emit(this.objPatient);
  }
  onCancel() {
    this.objPatient = new IPatient();
  }
}




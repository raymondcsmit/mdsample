import { Component, Inject, OnInit, Input,Output, EventEmitter, OnChanges, SimpleChange, SimpleChanges } from '@angular/core';
import { Observable, Observer } from 'rxjs';
import { IPatient } from '../../../model/patient';
@Component({
    selector: 'Patient-List',
    templateUrl: './Patientlist.html'
})
export class PatientListComponent implements OnInit,OnChanges {
@Input() listPatient: Observable<IPatient>;
   constructor(
	 
	) {
    }
ngOnInit() {
        // load any lookup type data for dropdown
            }
    ngOnChanges(changes: SimpleChanges) {
        if (changes.listPatient && !changes.listPatient.isFirstChange()) {
            // exteranl API call or more preprocessing...
            this.listPatient = changes.listPatient.currentValue;
        }

        //for (let propName in changes) {
        //    let change = changes['listPatient'];
        //    this.listPatient = change.currentValue;
        //    //if (change.isFirstChange()) {
        //    //    this.listPatient = change.currentValue;
        //    //} else {
        //    //    console.log('no change in data')
        //    //}
        //}
    }

@Output()
  notifySelect: EventEmitter<IPatient> = new EventEmitter<IPatient>();
  onSelect(patient: IPatient): void {
    this.notifySelect.emit(patient);
  }

  @Output()
  notifyDelete: EventEmitter<IPatient> = new EventEmitter<IPatient>();
  onDelete(patient: IPatient): void {
    this.notifyDelete.emit(patient);
  }
}




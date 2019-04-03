import { Component, Inject, OnInit, Input,Output, EventEmitter, OnChanges, SimpleChange, SimpleChanges } from '@angular/core';
import { Observable, Observer } from 'rxjs';
import { IPatient } from '../../../model/patient';
import { INextOfKin } from '../../../model/nextofkin';
import { PatientService } from '../../../services/Patient.service';
			     

@Component({
    selector: 'NextOfKin-List',
    templateUrl: './NextOfKinlist.html'
})
export class NextOfKinListComponent implements OnInit,OnChanges {

				public listPatient: IPatient[];
			        
   
@Input() listNextOfKin: Observable<INextOfKin>;
   
   constructor(
					private srvPatientService: PatientService,
			     
	) {
    }
ngOnInit() {
        // load any lookup type data for dropdown
        				this.srvPatientService.GetAll().subscribe(res => {
					this.listPatient = res;
				}, error => console.log(error));
			        }
    ngOnChanges(changes: SimpleChanges) {
        if (changes.listNextOfKin && !changes.listNextOfKin.isFirstChange()) {
            // exteranl API call or more preprocessing...
            this.listNextOfKin = changes.listNextOfKin.currentValue;
        }

        //for (let propName in changes) {
        //    let change = changes['listNextOfKin'];
        //    this.listNextOfKin = change.currentValue;
        //    //if (change.isFirstChange()) {
        //    //    this.listNextOfKin = change.currentValue;
        //    //} else {
        //    //    console.log('no change in data')
        //    //}
        //}
    }

@Output()
  notifySelect: EventEmitter<INextOfKin> = new EventEmitter<INextOfKin>();
  onSelect(nextofkin: INextOfKin): void {
    this.notifySelect.emit(nextofkin);
  }

  @Output()
  notifyDelete: EventEmitter<INextOfKin> = new EventEmitter<INextOfKin>();
  onDelete(nextofkin: INextOfKin): void {
    this.notifyDelete.emit(nextofkin);
  }
}




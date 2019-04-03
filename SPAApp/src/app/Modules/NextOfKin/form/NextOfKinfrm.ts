import { Component, Inject, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChange, SimpleChanges } from '@angular/core';
import { IPatient } from '../../../model/patient';
import { INextOfKin } from '../../../model/nextofkin';
import { PatientService } from '../../../services/Patient.service';

@Component({
  selector: 'NextOfKin-Form',
  templateUrl: './NextOfKinfrm.html'
})
export class NextOfKinFormComponent implements OnInit, OnChanges {

  public listPatient: IPatient[];


  @Input() objNextOfKin: INextOfKin;
  @Output() notify: EventEmitter<INextOfKin> = new EventEmitter<INextOfKin>();
  constructor(
    private srvPatientService: PatientService,

  ) {
    //private srvlkptBranchService: lkptBranchService,
  }
  ngOnInit() {
    // load any lookup type data for dropdown
    this.srvPatientService.GetAll().subscribe(res => {
      this.listPatient = res;
    }, error => console.log(error));
  }
  ngOnChanges(changes: SimpleChanges) {
    if (changes.objNextOfKin && !changes.objNextOfKin.isFirstChange()) {
      // exteranl API call or more preprocessing...
      this.objNextOfKin = changes.objNextOfKin.currentValue;
    }

    //for (let propName in changes) {
    //    let change = changes['objNextOfKin'];
    //    this.objNextOfKin = change.currentValue;
    //    //if (change.isFirstChange()) {
    //    //    this.objNextOfKin = change.currentValue;
    //    //} else {
    //    //    console.log('no change in data')
    //    //}
    //}
  }

  onSave() {
    this.notify.emit(this.objNextOfKin);
  }
  onCancel() {
    this.objNextOfKin = new INextOfKin();
  }
}




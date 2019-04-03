import { Component, Inject, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChange, SimpleChanges } from '@angular/core';
import { NgbDateStruct, NgbDateParserFormatter } from '@ng-bootstrap/ng-bootstrap';
import { IBasic } from '../../../model/basic';
import { PatientService } from '../../../services/Patient.service';
import { IPatient } from '../../../model/patient';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'Basic-Form',
  templateUrl: './Basicfrm.html'
})
export class BasicFormComponent implements OnInit, OnChanges {
  public listPatient: IPatient[];
  public DateOfBirthNg: NgbDateStruct;
  pipe = new DatePipe('en-US');
  @Input() objBasic: IBasic;
  @Output() notify: EventEmitter<IBasic> = new EventEmitter<IBasic>();
  constructor(
    private srvPatientService: PatientService,
    private ngbDateParserFormatter: NgbDateParserFormatter,
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
    if (changes.objBasic && !changes.objBasic.isFirstChange()) {
      // exteranl API call or more preprocessing...
      this.objBasic = changes.objBasic.currentValue;
      if (this.objBasic.DateOfBirth != undefined)
        this.DateOfBirthNg = this.ngbDateParserFormatter.parse(this.pipe.transform(this.objBasic.DateOfBirth, 'short'));
    }
  }
  onDateOfBirth(date: NgbDateStruct) {
    if (date != null) {
      this.DateOfBirthNg = date;
      this.objBasic.DateOfBirth = new Date(this.ngbDateParserFormatter.format(this.DateOfBirthNg));
    }
  }
  onSave() {
    this.notify.emit(this.objBasic);
  }
  onCancel() {
    this.objBasic = new IBasic();
  }
}




import { Component, Inject, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChange, SimpleChanges } from '@angular/core';
import { Observable, Observer } from 'rxjs';
import { IPatient } from '../../../model/patient';
import { IBasic } from '../../../model/basic';
import { PatientService } from '../../../services/Patient.service';

@Component({
  selector: 'Basic-List',
  templateUrl: './Basiclist.html'
})
export class BasicListComponent implements OnInit, OnChanges {
  public listPatient: IPatient[];
  @Input() listBasic: Observable<IBasic>;
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
    if (changes.listBasic && !changes.listBasic.isFirstChange()) {
      // exteranl API call or more preprocessing...
      this.listBasic = changes.listBasic.currentValue;
    }

    //for (let propName in changes) {
    //    let change = changes['listBasic'];
    //    this.listBasic = change.currentValue;
    //    //if (change.isFirstChange()) {
    //    //    this.listBasic = change.currentValue;
    //    //} else {
    //    //    console.log('no change in data')
    //    //}
    //}
  }

  @Output()
  notifySelect: EventEmitter<IBasic> = new EventEmitter<IBasic>();
  onSelect(basic: IBasic): void {
    this.notifySelect.emit(basic);
  }

  @Output()
  notifyDelete: EventEmitter<IBasic> = new EventEmitter<IBasic>();
  onDelete(basic: IBasic): void {
    this.notifyDelete.emit(basic);
  }
}




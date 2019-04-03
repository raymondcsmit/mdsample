import { Component, Inject, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChange, SimpleChanges } from '@angular/core';
import { Observable, Observer } from 'rxjs';
import { IPatient } from '../../../model/patient';
import { IGpDetail } from '../../../model/gpdetail';
import { PatientService } from '../../../services/Patient.service';


@Component({
  selector: 'GpDetail-List',
  templateUrl: './GpDetaillist.html'
})
export class GpDetailListComponent implements OnInit, OnChanges {

  public listPatient: IPatient[];
  @Input() listGpDetail: Observable<IGpDetail>;

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
    if (changes.listGpDetail && !changes.listGpDetail.isFirstChange()) {
      // exteranl API call or more preprocessing...
      this.listGpDetail = changes.listGpDetail.currentValue;
    }

    //for (let propName in changes) {
    //    let change = changes['listGpDetail'];
    //    this.listGpDetail = change.currentValue;
    //    //if (change.isFirstChange()) {
    //    //    this.listGpDetail = change.currentValue;
    //    //} else {
    //    //    console.log('no change in data')
    //    //}
    //}
  }

  @Output()
  notifySelect: EventEmitter<IGpDetail> = new EventEmitter<IGpDetail>();
  onSelect(gpdetail: IGpDetail): void {
    this.notifySelect.emit(gpdetail);
  }

  @Output()
  notifyDelete: EventEmitter<IGpDetail> = new EventEmitter<IGpDetail>();
  onDelete(gpdetail: IGpDetail): void {
    this.notifyDelete.emit(gpdetail);
  }
}




import { Component, Inject, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChange, SimpleChanges } from '@angular/core';
import { Observable, Observer, BehaviorSubject } from 'rxjs';
import { IPatient } from '../../../model/patient';
import { IGpDetail } from '../../../model/gpdetail';
import { take, map } from 'rxjs/operators';
import { GpDetailService } from '../../../services/GpDetail.service';


@Component({
  selector: 'GpDetail-List-Form',
  templateUrl: './GpDetaillistfrm.html'
})
export class GpDetailListFormComponent implements OnInit, OnChanges {

  public listPatient: IPatient[];
  @Input() listGpDetail: IGpDetail[];
  
  constructor(
    private srvGpDetailService: GpDetailService

  ) {
  }
  ngOnInit() {
    
  }
  ngOnChanges(changes: SimpleChanges) {
    if (changes.listGpDetail && !changes.listGpDetail.isFirstChange()) {
      // exteranl API call or more preprocessing...
      this.listGpDetail = changes.listGpDetail.currentValue;
    }
  }
  addNew() {
    let objGp = new IGpDetail();
    this.listGpDetail.push(objGp);   
    
  }
   onSave(gpdetail: IGpDetail): void {
    
  }
  //@Output()
  //notifySelect: EventEmitter<IGpDetail> = new EventEmitter<IGpDetail>();
  //onSelect(gpdetail: IGpDetail): void {
  //  this.notifySelect.emit(gpdetail);
  //}

  //@Output()
  //notifyDelete: EventEmitter<IGpDetail> = new EventEmitter<IGpDetail>();
  onCancel(objGp: IGpDetail): void {
    let index = this.listGpDetail.findIndex(c => c.GpCode == objGp.GpCode);
    if (index !== -1) {
      this.listGpDetail.splice(index, 1);
      if (objGp.GpDetailID != 0) {
        this.srvGpDetailService.Delete(objGp.GpDetailID);
      }
    }  
  }
}




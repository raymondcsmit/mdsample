import { Component, Inject, OnInit, Input,Output, EventEmitter, OnChanges, SimpleChange, SimpleChanges } from '@angular/core';
import { IPatient } from '../../../model/patient';
import { IGpDetail } from '../../../model/gpdetail';
import { PatientService } from '../../../services/Patient.service';

@Component({
    selector: 'GpDetail-Form',
    templateUrl: './GpDetailfrm.html'
})
export class GpDetailFormComponent implements OnInit,OnChanges {
    
					public listPatient: IPatient[];
			     
   
    @Input() objGpDetail: IGpDetail;
    @Output() notify: EventEmitter<IGpDetail> = new EventEmitter<IGpDetail>();
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
        if (changes.objGpDetail && !changes.objGpDetail.isFirstChange()) {
            // exteranl API call or more preprocessing...
            this.objGpDetail = changes.objGpDetail.currentValue;
        }

        //for (let propName in changes) {
        //    let change = changes['objGpDetail'];
        //    this.objGpDetail = change.currentValue;
        //    //if (change.isFirstChange()) {
        //    //    this.objGpDetail = change.currentValue;
        //    //} else {
        //    //    console.log('no change in data')
        //    //}
        //}
    }

   onSave() {
       this.notify.emit(this.objGpDetail);
   }
   onCancel() {
       this.objGpDetail = new IGpDetail();
   }
}




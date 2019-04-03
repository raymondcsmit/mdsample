import { Component, Inject, Input } from '@angular/core';
import { GpDetailService } from "../../services/GpDetail.service";
import { IGpDetail } from '../../model/gpdetail';
import { Observable } from 'rxjs';

@Component({
  selector: 'GpDetail-Container',
  templateUrl: './GpDetail.html'
})
export class GpDetailComponent {
  

  public objGpDetail: IGpDetail;
  public selectedGpDetail: IGpDetail;
  @Input() listGpDetail: IGpDetail[];
  constructor(private srvGpDetailService: GpDetailService) {

  }

  onNotify(objdata: IGpDetail): void {
    //	if (objdata.GpDetailID == 0) {
    //          this.srvGpDetailService.Add(objdata)
    //              .subscribe(res => {this.getData(); console.log('GpDetail Data Saved'); },
    //		error => {console.log('GpDetail Data could not be saved');console.log(error);});        
    //      }
    //      else {
    //          this.srvGpDetailService.Update(objdata.GpDetailID,objdata)
    //              .subscribe(res => {this.getData(); console.log('GpDetail Data Updated'); },
    //error => {console.log('GpDetail Data could not be Updated');        
    //                  console.log(error);
    //      });
    //      }
  }

  onSelect(objgpdetail: IGpDetail): void {
    this.objGpDetail = objgpdetail;
  }
  onDelete(objgpdetail: IGpDetail): void {
    //this.srvGpDetailService.Delete(objgpdetail.GpDetailID)
    // .subscribe(response => {
    //              this.getData();
    //		//this.notificationservice.success("Suceess", "Record [ID:"+objgpdetail.id+"] deleted successfully", {id: objgpdetail.id});
    //              //console.log("data delete success");
    //          },
    //          error => {
    //              this.getData();
    //		//this.notificationservice.error("Error", "Error deleting record [ID:"+objgpdetail.id+"]");
    //              //console.log("data delete error");
    //          });  
  }


}



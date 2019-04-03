import { Component, Inject } from '@angular/core';

import { NextOfKinService } from "../../services/NextOfKin.service";
import {INextOfKin} from '../../model/nextofkin';

@Component({
    selector: 'NextOfKin',
    templateUrl: './NextOfKin.html'
})
export class NextOfKinComponent {
    public listNextOfKin: INextOfKin[];
	public objNextOfKin: INextOfKin;
	public selectedNextOfKin: INextOfKin;
    constructor(private srvNextOfKinService: NextOfKinService) {
      this.getData();  
    }
	getData(): void {
        this.srvNextOfKinService.GetAll().subscribe(res => {
            this.listNextOfKin= res;
        }, error => console.log(error));

		this.objNextOfKin=new  INextOfKin();
		
    }
	onNotify(objdata: INextOfKin): void {               
			if (objdata.NextOfKinID == 0) {
            this.srvNextOfKinService.Add(objdata)
                .subscribe(res => {this.getData(); console.log('NextOfKin Data Saved'); },
				error => {console.log('NextOfKin Data could not be saved');console.log(error);});        
        }
        else {
            this.srvNextOfKinService.Update(objdata.NextOfKinID,objdata)
                .subscribe(res => {this.getData(); console.log('NextOfKin Data Updated'); },
		error => {console.log('NextOfKin Data could not be Updated');        
                    console.log(error);
        });
        }
		    }

	onSelect(objnextofkin: INextOfKin): void {
        this.objNextOfKin = objnextofkin;		
    }
	onDelete(objnextofkin: INextOfKin): void {
		this.srvNextOfKinService.Delete(objnextofkin.NextOfKinID)
		 .subscribe(response => {
                this.getData();
				//this.notificationservice.success("Suceess", "Record [ID:"+objnextofkin.id+"] deleted successfully", {id: objnextofkin.id});
                //console.log("data delete success");
            },
            error => {
                this.getData();
				//this.notificationservice.error("Error", "Error deleting record [ID:"+objnextofkin.id+"]");
                //console.log("data delete error");
            });  
    }


}



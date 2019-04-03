import { Component, Inject } from '@angular/core';

import { BasicService } from "../../services/Basic.service";
import {IBasic} from '../../model/basic';

@Component({
    selector: 'Basic',
    templateUrl: './Basic.html'
})
export class BasicComponent {
    public listBasic: IBasic[];
	public objBasic: IBasic;
	public selectedBasic: IBasic;
    constructor(private srvBasicService: BasicService) {
      this.getData();  
    }
	getData(): void {
        this.srvBasicService.GetAll().subscribe(res => {
            this.listBasic= res;
        }, error => console.log(error));

		this.objBasic=new  IBasic();
		
    }
	onNotify(objdata: IBasic): void {               
			if (objdata.BasicID == 0) {
            this.srvBasicService.Add(objdata)
                .subscribe(res => {this.getData(); console.log('Basic Data Saved'); },
				error => {console.log('Basic Data could not be saved');console.log(error);});        
        }
        else {
            this.srvBasicService.Update(objdata.BasicID,objdata)
                .subscribe(res => {this.getData(); console.log('Basic Data Updated'); },
		error => {console.log('Basic Data could not be Updated');        
                    console.log(error);
        });
        }
		    }

	onSelect(objbasic: IBasic): void {
        this.objBasic = objbasic;		
    }
	onDelete(objbasic: IBasic): void {
		this.srvBasicService.Delete(objbasic.BasicID)
		 .subscribe(response => {
                this.getData();
				//this.notificationservice.success("Suceess", "Record [ID:"+objbasic.id+"] deleted successfully", {id: objbasic.id});
                //console.log("data delete success");
            },
            error => {
                this.getData();
				//this.notificationservice.error("Error", "Error deleting record [ID:"+objbasic.id+"]");
                //console.log("data delete error");
            });  
    }


}



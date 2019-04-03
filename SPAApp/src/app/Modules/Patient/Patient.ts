import { Component, Inject, OnInit } from '@angular/core';

import { PatientService } from "../../services/Patient.service";
import { IPatient } from '../../model/patient';
import { IGpDetail } from '../../model/gpdetail';
import { IProperty } from '../../model/Property';

@Component({
  selector: 'Patient',
  templateUrl: './Patient.html'
})
export class PatientComponent implements OnInit{
  public listPatient: IPatient[];
  public objPatient: IPatient;
  public selectedPatient: IPatient;
  public showform: boolean;
  public page: number;
  public pagesize: number;

  constructor(private srvPatientService: PatientService) {
    this.page = 0;
    this.pagesize = 5;
    this.showform = false;
  }
  ngOnInit() {
    this.showList();
  }
  getData(): void {
    //this.srvPatientService.GetAll().subscribe(res => {
    //  this.listPatient = res;
    //}, error => console.log(error));
    this.srvPatientService.GetPatientPaged(this.page, this.pagesize).subscribe(res => {
      this.listPatient = res;
    }, error => console.log(error));
    this.newPatient();
  }
  newPatient(): void {
    this.objPatient = new IPatient();
    
  }
  onNotify(objdata: IPatient): void {
    if (objdata.PatientID == 0) {
      this.srvPatientService.Add(objdata)
        .subscribe(res => { this.showList(); console.log('Patient Data Saved'); },
          error => { console.log('Patient Data could not be saved'); console.log(error); });
    }
    else {
      this.srvPatientService.Update(objdata.PatientID, objdata)
        .subscribe(res => { this.showList(); console.log('Patient Data Updated'); },
          error => {
            console.log('Patient Data could not be Updated');
            console.log(error);
          });
    }
  }
  addNew(): void {
    this.showform = true;
    this.newPatient();
  }
  showList(): void {
    this.showform = false;
    this.getData();
  }
  onSelect(objpatient: IPatient): void {
    this.objPatient = objpatient;
    this.showform = true;
  }
  onSearch(objListSearch: IProperty[]) {
    this.srvPatientService.GetSearch(objListSearch).subscribe(res => {
      this.listPatient = res;
    }, error => console.log(error));
  }
  onDelete(objpatient: IPatient): void {
    this.srvPatientService.Delete(objpatient.PatientID)
      .subscribe(response => {
        this.getData();
        //this.notificationservice.success("Suceess", "Record [ID:"+objpatient.id+"] deleted successfully", {id: objpatient.id});
        //console.log("data delete success");
      },
        error => {
          this.getData();
          //this.notificationservice.error("Error", "Error deleting record [ID:"+objpatient.id+"]");
          //console.log("data delete error");
        });
  }


}



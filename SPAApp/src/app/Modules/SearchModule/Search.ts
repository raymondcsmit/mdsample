import { Component, Inject, OnInit, Output, EventEmitter } from '@angular/core';

import { PatientService } from "../../services/Patient.service";
import { IPatient } from '../../model/patient';
import { IGpDetail } from '../../model/gpdetail';
import { IProperty } from '../../model/Property';

@Component({
  selector: 'Search',
  templateUrl: './Search.html'
})
export class SearchComponent implements OnInit{
  public searchModel : IProperty[]=[];
  //{
  //  'property_res': [{ 'property': '', 'action': 'contains', 'value': '', 'logical_operator': 'and' }]
  //}
  public listProperties = ["FkBasic.Forenames", "FkBasic.Surname"];
  public listAction = ["doesn't contain", "contains"];
  public listOperator = ["or", "and"];
  constructor() {    
  }
  ngOnInit() {
    
    this.addNew();
  }  
  addNew(): void {
    this.searchModel.push(new IProperty());
  } 
  @Output()
  notifySearch: EventEmitter<IProperty[]> = new EventEmitter<IProperty[]>();
  search(): void {
    this.notifySearch.emit(this.searchModel);
  }
}



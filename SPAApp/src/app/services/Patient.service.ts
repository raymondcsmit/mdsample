import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, Observer } from 'rxjs';
import { IPatient } from '../model/patient';
import { ParentService } from "./parent.service";
import { IProperty } from '../model/Property';

@Injectable()
export class PatientService extends ParentService {

  public listpatient: IPatient[];
  public objpatient: IPatient;
  private actionUrl: string;
  constructor(http: HttpClient) {
    super(http);
    this.actionUrl = 'http://localhost:5410/api/patient/';
  }
  public GetPatientPaged(p:number,psize:number): Observable<IPatient[]> {
  //   let toAdd =  encodeURI(JSON.stringify({ page: p, pagesize: psize }));
  //   return this.http.post<IPatient[]>(this.actionUrl + "getpatientpaged", toAdd);
  // return this.http.get<IPatient[]>(this.actionUrl);
  return this.http.get<IPatient[]>(this.actionUrl);
}
  public GetAll(): Observable<IPatient[]> {
    return this.http.get<IPatient[]>(this.actionUrl);
  }

  public GetSearch(data: IProperty[]): Observable<IPatient[]> {
    let bar =  JSON.stringify(data);
    // let toAdd = new HttpParams();
    // toAdd = toAdd.set('prop', bar);
    
    //http.post('/api/GetDetails', body).subscribe();


    //let toAdd = JSON.stringify(data);
    return this.http.post<IPatient[]>(this.actionUrl + "getsearch", bar);
  }
  public GetSingle(id: number): Observable<IPatient> {
    return this.http.get<IPatient>(this.actionUrl + id);
  }

  public Add(data: IPatient): Observable<IPatient> {
    let toAdd = JSON.stringify(data);
    return this.http.post<IPatient>(this.actionUrl, toAdd);
  }

  public Update(id: number, itemToUpdate: any): Observable<IPatient> {
    return this.http.put<IPatient>(this.actionUrl + id, JSON.stringify(itemToUpdate));
  }

  public Delete(id: number): Observable<IPatient> {
    return this.http.delete<IPatient>(this.actionUrl + id);
  }
}

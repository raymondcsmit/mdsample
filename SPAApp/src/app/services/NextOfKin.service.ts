import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, Observer } from 'rxjs';
import {INextOfKin} from '../model/nextofkin';
import { ParentService } from "./parent.service";

@Injectable()
export class NextOfKinService extends ParentService {

public listnextofkin: INextOfKin[];
public objnextofkin: INextOfKin;
private actionUrl: string;
    constructor(http: HttpClient) {
	super(http);
	this.actionUrl = 'api/nextofkin/';        
    }

    public GetAll(): Observable<INextOfKin[]>  {	
	return this.http.get<INextOfKin[]>(this.actionUrl);
    }

    public GetSingle(id: number): Observable<INextOfKin>  {
        return this.http.get<INextOfKin>(this.actionUrl + id);		
    }

    public Add(data: INextOfKin): Observable<INextOfKin>  {
        var toAdd = JSON.stringify( data );
        return this.http.post<INextOfKin>(this.actionUrl, toAdd);
    }

    public Update(id: number, itemToUpdate: any): Observable<INextOfKin>  {
        return this.http.put<INextOfKin>(this.actionUrl + id, JSON.stringify(itemToUpdate));
    }

    public Delete(id: number): Observable<INextOfKin>  {
        return this.http.delete<INextOfKin>(this.actionUrl + id);
    }
}
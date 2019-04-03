import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, Observer } from 'rxjs';
import {IGpDetail} from '../model/gpdetail';
import { ParentService } from "./parent.service";

@Injectable()
export class GpDetailService extends ParentService {

public listgpdetail: IGpDetail[];
public objgpdetail: IGpDetail;
private actionUrl: string;
    constructor(http: HttpClient) {
	super(http);
	this.actionUrl = 'http://localhost:29164/api/gpdetail/';        
    }

    public GetAll(): Observable<IGpDetail[]>  {	
	return this.http.get<IGpDetail[]>(this.actionUrl);
    }

    public GetSingle(id: number): Observable<IGpDetail>  {
        return this.http.get<IGpDetail>(this.actionUrl + id);		
    }

    public Add(data: IGpDetail): Observable<IGpDetail>  {
        var toAdd = JSON.stringify( data );
        return this.http.post<IGpDetail>(this.actionUrl, toAdd);
    }

    public Update(id: number, itemToUpdate: any): Observable<IGpDetail>  {
        return this.http.put<IGpDetail>(this.actionUrl + id, JSON.stringify(itemToUpdate));
    }

    public Delete(id: number): Observable<IGpDetail>  {
        return this.http.delete<IGpDetail>(this.actionUrl + id);
    }
}
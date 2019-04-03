import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, Observer } from 'rxjs';
import {IBasic} from '../model/basic';
import { ParentService } from "./parent.service";

@Injectable()
export class BasicService extends ParentService {

public listbasic: IBasic[];
public objbasic: IBasic;
private actionUrl: string;
    constructor(http: HttpClient) {
	super(http);
	this.actionUrl = 'http://localhost:5410/api/basic/';        
    }

    public GetAll(): Observable<IBasic[]>  {	
	return this.http.get<IBasic[]>(this.actionUrl);
    }

    public GetSingle(id: number): Observable<IBasic>  {
        return this.http.get<IBasic>(this.actionUrl + id);		
    }

    public Add(data: IBasic): Observable<IBasic>  {
        var toAdd = JSON.stringify( data );
        return this.http.post<IBasic>(this.actionUrl, toAdd);
    }

    public Update(id: number, itemToUpdate: any): Observable<IBasic>  {
        return this.http.put<IBasic>(this.actionUrl + id, JSON.stringify(itemToUpdate));
    }

    public Delete(id: number): Observable<IBasic>  {
        return this.http.delete<IBasic>(this.actionUrl + id);
    }
}

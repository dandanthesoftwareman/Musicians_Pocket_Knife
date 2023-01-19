import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { UserService } from './user.service';


@Injectable({
  providedIn: 'root'
})
export class SongService {

  constructor(private http:HttpClient, @Inject("BASE_URL") private baseUrl:string) { }

  endpoint:string = "api/Song"

  TransposeUp(apiid:string, listTitle:string, newKey:string):any{
    return this.http.patch(`${this.baseUrl}${this.endpoint}/TransposeUp?apiid=${apiid}&listTitle=${listTitle}&newKey=${newKey}&id=${UserService.user.id}`, {});
  }
  TransposeDown(apiid:string, listTitle:string, newKey:string):any{
    return this.http.patch(`${this.baseUrl}${this.endpoint}/TransposeDown?apiid=${apiid}&listTitle=${listTitle}&newKey=${newKey}&id=${UserService.user.id}`, {});
  }
}

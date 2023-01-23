import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { DbSong } from './db-song';
import { UserService } from './user.service';


@Injectable({
  providedIn: 'root'
})
export class SongService {

  constructor(private http:HttpClient, @Inject("BASE_URL") private baseUrl:string) { }

  endpoint:string = "api/Song"

  SaveTransposeChanges(songs:DbSong[], listTitle:string):any{
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(songs);
    return this.http.patch(`${this.baseUrl}${this.endpoint}/SaveTransposeChanges`, body, {'headers': headers})
  }
}

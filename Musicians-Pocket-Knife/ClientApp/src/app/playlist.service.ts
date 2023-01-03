import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PlaylistService {

  constructor(private http:HttpClient, @Inject("BASE_URL") private baseUrl:string) { }

  endpoint:string = "api/Playlist";

  CreatePlaylist(title:string, id:string):any{
    return this.http.post(`${this.baseUrl}${this.endpoint}/CreatePlaylist?ListTitle=${title}?UserId=${id}`,{});
  }
}

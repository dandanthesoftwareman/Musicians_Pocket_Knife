import { SocialUser } from '@abacritt/angularx-social-login';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class PlaylistService {

  constructor(private http:HttpClient, @Inject("BASE_URL") private baseUrl:string) { }

  endpoint:string = "api/Playlist";

  CreatePlaylist(title:string):any{
    return this.http.post(`${this.baseUrl}${this.endpoint}/CreatePlaylist?title=${title}&id=${UserService.user.id}`,{});
  }

  GetUserPlaylists(id:string):any{
    return this.http.get(`${this.baseUrl}${this.endpoint}/GetUserPlaylists?&id=${UserService.user.id}`,{})
  }
}

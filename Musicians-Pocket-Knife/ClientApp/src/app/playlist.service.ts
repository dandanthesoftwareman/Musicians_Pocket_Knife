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
    return this.http.post(`${this.baseUrl}${this.endpoint}/CreatePlaylist?listTitle=${title}&id=${UserService.user.id}`,{});
  }

  GetUserPlaylists():any{
    return this.http.get(`${this.baseUrl}${this.endpoint}/GetUserPlaylists?&id=${UserService.user.id}`);
  }

  ViewPlaylistDetails(listTitle:string):any{
    return this.http.get(`${this.baseUrl}${this.endpoint}/ViewPlaylistDetails?title=${listTitle}&id=${UserService.user.id}`);
  }

  AddSongToPlaylist(songID:string, listTitle:string):any{
    return this.http.post(`${this.baseUrl}${this.endpoint}/AddSongToPlaylist?songID=${songID}&id=${UserService.user.id}&listTitle=${listTitle}`, {})
  }

  RemoveSongFromPlaylist(songID:number, listTitle:string):any{
    return this.http.delete(`${this.baseUrl}${this.endpoint}/RemoveSongFromPlaylist?songID=${songID}&id=${UserService.user.id}&listTitle=${listTitle}`)
  }
}

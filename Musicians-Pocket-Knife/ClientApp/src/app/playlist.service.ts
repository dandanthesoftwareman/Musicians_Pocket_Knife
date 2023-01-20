import { SocialUser } from '@abacritt/angularx-social-login';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Song } from './song';
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

  DeletePlaylist(title:string):any{
    return this.http.delete(`${this.baseUrl}${this.endpoint}/DeletePlaylist?listTitle=${title}&id=${UserService.user.id}`);
  }

  GetUserPlaylists():any{
    return this.http.get(`${this.baseUrl}${this.endpoint}/GetUserPlaylists?&id=${UserService.user.id}`);
  }

  ViewPlaylistDetails(listTitle:string):any{
    return this.http.get(`${this.baseUrl}${this.endpoint}/ViewPlaylistDetails?title=${listTitle}&id=${UserService.user.id}`);
  }

  AddSongToPlaylist(song:Song, listTitle:string):any{
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(song);
    return this.http.post(`${this.baseUrl}${this.endpoint}/AddSongToPlaylist?id=${UserService.user.id}&listTitle=${listTitle}`, body, {'headers': headers})
  }

  RemoveSongFromPlaylist(songID:number, listTitle:string):any{
    return this.http.delete(`${this.baseUrl}${this.endpoint}/RemoveSongFromPlaylist?songID=${songID}&id=${UserService.user.id}&listTitle=${listTitle}`)
  }

  RenamePlaylist(oldTitle:string, newtitle:string):any{
    return this.http.patch(`${this.baseUrl}${this.endpoint}/RenamePlaylist?oldTitle=${oldTitle}&newTitle=${newtitle}&id=${UserService.user.id}`, {})
  }
}

import { SocialUser } from '@abacritt/angularx-social-login';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { DbSong } from './db-song';
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

  DeletePlaylist(listId:number):any{
    return this.http.delete(`${this.baseUrl}${this.endpoint}/DeletePlaylist?listId=${listId}&id=${UserService.user.id}`);
  }

  GetUserPlaylists():any{
    return this.http.get(`${this.baseUrl}${this.endpoint}/GetUserPlaylists?&id=${UserService.user.id}`);
  }

  GetListTitle(listId:number):any{
    return this.http.get(`${this.baseUrl}${this.endpoint}/GetListTitle?listId=${listId}&id=${UserService.user.id}`);
  }

  ViewPlaylistDetails(listId:number):any{
    return this.http.get(`${this.baseUrl}${this.endpoint}/ViewPlaylistDetails?listId=${listId}&id=${UserService.user.id}`);
  }

  AddSongToPlaylist(song:Song, listId:number):any{
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(song);
    return this.http.post(`${this.baseUrl}${this.endpoint}/AddSongToPlaylist?id=${UserService.user.id}&listId=${listId}`, body, {'headers': headers})
  }

  RemoveSongFromPlaylist(songID:number, listId:number):any{
    return this.http.delete(`${this.baseUrl}${this.endpoint}/RemoveSongFromPlaylist?songID=${songID}&id=${UserService.user.id}&listId=${listId}`)
  }

  RenamePlaylist(listId:number, newtitle:string):any{
    return this.http.patch(`${this.baseUrl}${this.endpoint}/RenamePlaylist?listId=${listId}&newTitle=${newtitle}&id=${UserService.user.id}`, {})
  }

  UpdateDateViewed(listId:number):any{
    return this.http.patch(`${this.baseUrl}${this.endpoint}/UpdateDateViewed?listId=${listId}&id=${UserService.user.id}`, {})
  }

  UpdateSongIndexes(songs:DbSong[]):any{
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(songs);
    return this.http.patch(`${this.baseUrl}${this.endpoint}/UpdateSongIndexes`, body, {'headers': headers})
  }
}


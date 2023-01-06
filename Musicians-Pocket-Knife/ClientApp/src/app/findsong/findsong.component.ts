import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { SongArray } from '../song-array';
import { Song } from '../song';
import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { UserService } from '../user.service';
import { ApiService } from '../api.service';
import { PlaylistService } from '../playlist.service';
import { Playlist } from '../playlist';


@Component({
  selector: 'app-findsong',
  templateUrl: './findsong.component.html',
  styleUrls: ['./findsong.component.css']
})
export class FindsongComponent implements OnInit {

  song:Song = {} as Song;
  songArray:SongArray = {} as SongArray;
  array = {} as Song[];

  user: SocialUser = {} as SocialUser;
  loggedIn: boolean = false;
  userPlaylists:Playlist[] = {} as Playlist[];
  displayPlaylistForm:Boolean = false;

  constructor(private apiService: ApiService, private authService:SocialAuthService, private playlistService:PlaylistService) { }

  ngOnInit(): void {
    this.authService.authState.subscribe((user) => {
      this.user = user;
      UserService.user.id = user.id;
      this.loggedIn = (user != null);
  })
  this.playlistService.GetUserPlaylists().subscribe((response:any) => {
    this.userPlaylists = response;})
}
  
  //uses name=searched song of searchSongForm, trims and formats it to string for API call
  searchForSong(form:NgForm):SongArray{
    let song = form.form.value.searchedSong.trim().replaceAll(' ', '+');
    return this.apiService.getSongArray(song).subscribe((response:any) => {
      this.songArray = response;
    });
  }

  AddToPlaylist(listTitle:string):any {
    let songID = this.song.song.id;
    this.playlistService.AddSongToPlaylist(songID, listTitle).subscribe((response:any)=> {
      console.log("song 'should' be added");
    });
  }
  
  ShowPlaylists(id:string):void{
    this.displayPlaylistForm = !this.displayPlaylistForm;
    this.apiService.getSongInfo(id).subscribe((response:Song) => {
      this.song = response;
    })
    console.log("Button Clicked");
  }
}

import { Component, Inject, OnInit } from '@angular/core';
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
  songId:string = "";
  songArray:any;
  array = {} as Song[];

  user: SocialUser = {} as SocialUser;
  loggedIn: boolean = false;
  userPlaylists:Playlist[] = {} as Playlist[];
  displayPlaylistForm:Boolean = false;
  visible = false;
  constructor(private apiService: ApiService, private authService:SocialAuthService, private playlistService:PlaylistService) { }

  ngOnInit(): void {
    this.authService.authState.subscribe((user) => {
      this.user = user;
      UserService.user.id = user.id;
      this.loggedIn = (user != null);
  })
  this.playlistService.GetUserPlaylists().subscribe((response:any) => {
    this.userPlaylists = response;
    this.userPlaylists.sort((a,b) => (a.lastDateViewed > b.lastDateViewed) ? -1: 1);})
}
  
  //uses name=searched song of searchSongForm, trims and formats it to string for API call
  searchForSong(form:NgForm):SongArray{
    let song = form.form.value.searchedSong.trim().replaceAll(' ', '+');
    return this.apiService.getSongArray(song).subscribe((response:any) => {
      this.songArray = response;
      form.form.reset();
    });
  }

  AddToPlaylist(listTitle:string):any {
    this.playlistService.AddSongToPlaylist(this.song, listTitle).subscribe((response:any)=> {
      console.log(response);
    });
    this.ToggleDisplayPlaylistForm();
  }
  
  ShowPlaylists(id:string):void{
    this.ToggleDisplayPlaylistForm();
    if(this.songId != id){
      this.apiService.getSongDetails(id).subscribe((response:Song) => {
        this.song = response;
        this.songId = this.song.song.id;
      })
    }
  }

  ToggleDisplayPlaylistForm(){
    this.displayPlaylistForm = !this.displayPlaylistForm;
  }

  GetSongDetails(id:string){
    if(this.songId != id){
      if(this.visible == true){
        this.songId = "";
        this.toggleCollapse();
      }
      if(this.visible == false){
        this.apiService.getSongDetails(id).subscribe((response:Song) => {
          this.song = response;
          this.songId = this.song.song.id;
          this.toggleCollapse();
        });
      }
    }
    if(this.songId == id){
      this.toggleCollapse();
    }
  }

  toggleCollapse(): void {
    this.visible = !this.visible;
  }

  ClearSearch(){
    this.songArray = [];
    if(this.displayPlaylistForm == true){
      this.displayPlaylistForm = false;
    }
  }
}

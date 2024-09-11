import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Playlist } from '../interfaces/playlist';
import { PlaylistService } from '../playlist.service';
import { UserService } from '../user.service';

@Component({
  selector: 'app-playlists',
  templateUrl: './playlists.component.html',
  styleUrls: ['./playlists.component.css']
})
export class PlaylistsComponent implements OnInit {

  constructor(private authService: SocialAuthService, private playlistService: PlaylistService, private changeDetection: ChangeDetectorRef) { }

  user: SocialUser = {} as SocialUser;
  loggedIn: boolean = false;

  userPlaylists:Playlist[] = {} as Playlist[];
  toggleCreatePlaylist:boolean = false;
  toggleDeletePlaylist:boolean = false;
  togglePlaylistOptions:boolean = false;

  ngOnInit(): void {
    this.authService.authState.subscribe((user) => {
      this.user = user;
      UserService.user.id = user.id;
      this.loggedIn = (user != null);
    });

    this.playlistService.GetUserPlaylists().subscribe((response:any) => {
      this.userPlaylists = response;
      this.userPlaylists.sort((a,b) => (a.lastDateViewed > b.lastDateViewed) ? -1: 1);
    });
  }

  CreatePlaylist(form:NgForm):any{
    this.playlistService.CreatePlaylist(form.form.value.PlaylistTitle).subscribe((response:any) => {
      this.userPlaylists = [];
      this.GetUserPlaylists();
      this.changeDetection.detectChanges();
      form.form.reset();
    });
  }

  GetUserPlaylists():any{
    this.playlistService.GetUserPlaylists().subscribe((response:any) => {
      this.userPlaylists = response;
    });
  }

  DeletePlaylist(listId:number):any{
    this.playlistService.DeletePlaylist(listId).subscribe((response:any) =>{
      this.userPlaylists = [];
      this.GetUserPlaylists();
      this.changeDetection.detectChanges();
    });
  }

  ToggleCreatePlaylist(){
    this.toggleCreatePlaylist = !this.toggleCreatePlaylist;
    this.togglePlaylistOptions = !this.togglePlaylistOptions;
  }

  ToggleDeletePlaylist(){
    this.toggleDeletePlaylist = !this.toggleDeletePlaylist;
    this.togglePlaylistOptions = !this.togglePlaylistOptions;
  }

  UpdateDateViewed(listId:number){
    this.playlistService.UpdateDateViewed(listId).subscribe((response:any)=>{
    });
  }
}
import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Playlist } from '../playlist';
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

  userPlaylists:any;
  toggleDeletePlaylist:boolean = false;

  ngOnInit(): void {
    this.authService.authState.subscribe((user) => {
      this.user = user;
      UserService.user.id = user.id;
      this.loggedIn = (user != null);
    });

    this.playlistService.GetUserPlaylists().subscribe((response:any) => {
      this.userPlaylists = response;
    });
  }

  CreatePlaylist(form:NgForm):any{
    this.playlistService.CreatePlaylist(form.form.value.PlaylistTitle).subscribe((response:any) => {
      this.userPlaylists = [];
      this.GetUserPlaylists();
      this.changeDetection.detectChanges();
    });
  }

  GetUserPlaylists():any{
    this.playlistService.GetUserPlaylists().subscribe((response:any) => {
      this.userPlaylists = response;
      console.log(response);
    });
  }

  DeletePlaylist(listId:number):any{
    this.playlistService.DeletePlaylist(listId).subscribe((resppnse:any) =>{
      this.userPlaylists = [];
      this.GetUserPlaylists();
      this.changeDetection.detectChanges();
    });
  }

  ToggleDeletePlaylist():any{
    this.toggleDeletePlaylist = !this.toggleDeletePlaylist;
  }
}
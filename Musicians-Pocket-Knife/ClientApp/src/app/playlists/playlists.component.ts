import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { PlaylistService } from '../playlist.service';
import { UserService } from '../user.service';

@Component({
  selector: 'app-playlists',
  templateUrl: './playlists.component.html',
  styleUrls: ['./playlists.component.css']
})
export class PlaylistsComponent implements OnInit {

  constructor(private authService: SocialAuthService, private playlistService: PlaylistService) { }

  user: SocialUser = {} as SocialUser;
  loggedIn: boolean = false;

  ngOnInit(): void {
    this.authService.authState.subscribe((user) => {
      this.user = user;
      UserService.user.id = user.id;
      this.loggedIn = (user != null);
    });
    this.playlistService.GetUserPlaylists().subscribe((response:any) => {
      console.log(response);
    })
}
  CreatePlaylist(form:NgForm):any{
    this.playlistService.CreatePlaylist(form.form.value.PlaylistTitle).subscribe((response:any) => {
      console.log(response);
    });
  }
}
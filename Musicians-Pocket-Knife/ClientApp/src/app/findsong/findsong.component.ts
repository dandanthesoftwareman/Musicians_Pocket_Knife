import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { SongArray } from '../song-array';
import { Song } from '../song';
import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { UserService } from '../user.service';
import { ApiService } from '../api.service';


@Component({
  selector: 'app-findsong',
  templateUrl: './findsong.component.html',
  styleUrls: ['./findsong.component.css']
})
export class FindsongComponent implements OnInit {

  song:Song = {} as Song;
  songArray:SongArray = {} as SongArray;

  user: SocialUser = {} as SocialUser;
  loggedIn: boolean = false;

  constructor(private apiService: ApiService, private authService:SocialAuthService) { }

  ngOnInit(): void {
    this.authService.authState.subscribe((user) => {
      this.user = user;
      UserService.user.id = user.id;
      this.loggedIn = (user != null);
  })
}
  
  //uses name=searched song of searchSongForm, trims and formats it to string for API call
  searchForSong(form:NgForm):SongArray{
    let song = form.form.value.searchedSong.trim().replaceAll(' ', '+');
    return this.apiService.getSongArray(song).subscribe((response:any) => {
      this.songArray = response;
    });
  }

  AddToPlaylist(id:string):any {
    return null;
  }
}

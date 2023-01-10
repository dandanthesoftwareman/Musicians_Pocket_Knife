import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../api.service';
import { Song } from '../song';
import { UserService } from '../user.service';

@Component({
  selector: 'app-song-details',
  templateUrl: './song-details.component.html',
  styleUrls: ['./song-details.component.css']
})
export class SongDetailsComponent implements OnInit {

  constructor(private authService:SocialAuthService, private apiService: ApiService, private route:ActivatedRoute) { }

  user: SocialUser = {} as SocialUser;
  loggedIn: boolean = false;
  song:Song = {} as Song;
  songId: string = "";

  ngOnInit(): void {
    this.authService.authState.subscribe((user) => {
      this.user = user;
      UserService.user.id = user.id;
      this.loggedIn = (user != null);
  })
  let params = this.route.snapshot.paramMap;
  this.songId = String(params.get("id"));
  this.apiService.getSongDetails(this.songId).subscribe((response:Song) =>{
    this.song = response;
  })
  }

}

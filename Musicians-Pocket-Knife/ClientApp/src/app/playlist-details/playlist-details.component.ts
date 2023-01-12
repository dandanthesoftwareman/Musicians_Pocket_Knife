import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DbSong } from '../db-song';
import { PlaylistService } from '../playlist.service';
import { UserService } from '../user.service';

@Component({
  selector: 'app-playlist-details',
  templateUrl: './playlist-details.component.html',
  styleUrls: ['./playlist-details.component.css']
})
export class PlaylistDetailsComponent implements OnInit {

  constructor(private http:HttpClient, private authService:SocialAuthService, private playlistService: PlaylistService, private route: ActivatedRoute) { }

  user: SocialUser = {} as SocialUser;
  loggedIn: boolean = false;

  listTitle: string =  "";
  listSongs: DbSong[] = {} as DbSong[];

  ngOnInit(): void {
    this.authService.authState.subscribe((user) => {
      this.user = user;
      UserService.user.id = user.id;
      this.loggedIn = (user != null);
  })
  let params = this.route.snapshot.paramMap;
      this.listTitle = String(params.get("listTitle"));
      this.playlistService.ViewPlaylistDetails(this.listTitle).subscribe((response:DbSong[])=>{
        this.listSongs = response;
        console.log(this.listSongs);
      })
}

RemoveSongFromPlaylist(songID:number):void{
  this.playlistService.RemoveSongFromPlaylist(songID, this.listTitle).subscribe((response:any)=>{
  })
}
}

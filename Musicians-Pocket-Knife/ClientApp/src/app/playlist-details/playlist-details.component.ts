import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
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

  constructor(private http:HttpClient, private authService:SocialAuthService, private playlistService: PlaylistService,
    private route: ActivatedRoute, private changeDetection: ChangeDetectorRef) { }

  user: SocialUser = {} as SocialUser;
  loggedIn: boolean = false;

  listTitle: string =  "";
  listSongs: DbSong[] = {} as DbSong[];

  sharpKeys: string[] = ["A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#"];
  //so far API only uses sharp keys, leaving flat keys array here for later use if need be
  //flatKeys: string[] = ["Ab", "A", "Bb", "B", "C", "Db", "D", "Eb", "E", "F", "Gb", "G"];

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
    this.listSongs = [];
    this.playlistService.ViewPlaylistDetails(this.listTitle).subscribe((response:any)=>{
      this.listSongs = response;
    });
    this.changeDetection.detectChanges();
  });
}

TransposeDown(transposedKey:string, songID:string):void{
  let key:string = "";
  let keyIndex:number;
  if(transposedKey.includes("m")){
    key = transposedKey.substring(0,transposedKey.indexOf("m"));
    keyIndex = this.sharpKeys.indexOf(key)-1;
    if(keyIndex < 0){
      keyIndex += 12;
    }
    if(keyIndex > 12){
      keyIndex -= 12;
    }
    key = this.sharpKeys[keyIndex] + "m";
    console.log(key);
  }
  else{
    key = transposedKey;
    keyIndex = this.sharpKeys.indexOf(key)-1;
    if(keyIndex < 0){
      keyIndex += 12;
    }
    if(keyIndex > 12){
      keyIndex -= 12;
    }
    key = this.sharpKeys[keyIndex];
    console.log(key);
  }
}

TransposeUp(transposedKey:string, songID:string):void{
  let key:string = "";
  let keyIndex:number;
  if(transposedKey.includes("m")){
    key = transposedKey.substring(0,transposedKey.indexOf("m"));
    keyIndex = this.sharpKeys.indexOf(key) +1;
    if(keyIndex < 0){
      keyIndex += 12;
    }
    if(keyIndex > 12){
      keyIndex -= 12;
    }
    key = this.sharpKeys[keyIndex] + "m";
    console.log(key);
  }
  else{
    key = transposedKey;
    keyIndex = this.sharpKeys.indexOf(key)+1;
    if(keyIndex < 0){
      keyIndex += 12;
    }
    if(keyIndex > 12){
      keyIndex -= 12;
    }
    key = this.sharpKeys[keyIndex];
    console.log(key);
  }
}
}

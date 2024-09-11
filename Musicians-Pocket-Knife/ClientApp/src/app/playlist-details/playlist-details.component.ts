import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { DbSong } from '../db-song';
import { PlaylistService } from '../playlist.service';
import { SongService } from '../song.service';
import { UserService } from '../user.service';
import {CdkDragDrop, moveItemInArray} from '@angular/cdk/drag-drop';


@Component({
  selector: 'app-playlist-details',
  templateUrl: './playlist-details.component.html',
  styleUrls: ['./playlist-details.component.css']
})
export class PlaylistDetailsComponent implements OnInit {

  constructor(private http:HttpClient, private authService:SocialAuthService, private playlistService: PlaylistService, private songService:SongService,
    private route: ActivatedRoute, private changeDetection: ChangeDetectorRef) { }

  user: SocialUser = {} as SocialUser;
  loggedIn: boolean = false;

  TransposeEnabled:boolean = false;
  toggleRenamePlaylist:boolean = false;
  toggleRemoveSong:boolean = false;
  hideOptions:boolean = false;
  toggleReOrderList:boolean = false;

  //LIST PROPERTIES
  listId:number = {} as number;
  listTitle: string =  "";
  listSongs: DbSong[] = {} as DbSong[];
  sharpKeys: string[] = ["A", "A♯", "B", "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯"];

    //so far the API only uses sharp keys, leaving flat keys array here for later use if need be
    //flatKeys: string[] = ["Ab", "A", "Bb", "B", "C", "Db", "D", "Eb", "E", "F", "Gb", "G"];

  ngOnInit(): void {
    this.authService.authState.subscribe((user) => {
      this.user = user;
      UserService.user.id = user.id;
      this.loggedIn = (user != null);
  })

  let params = this.route.snapshot.paramMap;
      let listId = Number(params.get("listId"));
      this.listId = listId;
      this.playlistService.GetListTitle(listId).subscribe((response:any) => {
        this.listTitle = response.listTitle;
      })
      this.playlistService.ViewPlaylistDetails(listId).subscribe((response:DbSong[])=>{
        this.listSongs = response;
      })
      this.playlistService.UpdateDateViewed(listId).subscribe((response:any)=>{
      })
}

RenamePlaylist(form:NgForm):any{
  this.ToggleRenamePlaylist();
  let newTitle = form.form.value.newListTitle;
  return this.playlistService.RenamePlaylist(this.listId, newTitle).subscribe((response:any) => {
    this.listTitle = newTitle;
    this.changeDetection.detectChanges();
  })
}

RemoveSongFromPlaylist(songID:number):void{
  this.playlistService.RemoveSongFromPlaylist(songID, this.listId).subscribe((response:any)=>{
    this.listSongs = [];
    this.playlistService.ViewPlaylistDetails(this.listId).subscribe((response:any)=>{
      this.listSongs = response;
      this.UpdateSongIndexes();
    });
  });
  this.changeDetection.detectChanges();
}

UpdateSongIndexes(){
  for(let i = 0; i < this.listSongs.length; i++){
    this.listSongs[i].songIndex = i;
  }
  this.playlistService.UpdateSongIndexes(this.listSongs).subscribe((response:any) => {
  });
}

//DISPLAY OPTIONS METHODS
ToggleRenamePlaylist(){
  this.toggleRenamePlaylist = !this.toggleRenamePlaylist;
  this.hideOptions = !this.hideOptions;
}

EnableTranspose(){
  this.TransposeEnabled = !this.TransposeEnabled;
  this.hideOptions = !this.hideOptions;
}

ToggleRemoveSong(){
  this.toggleRemoveSong = !this.toggleRemoveSong;
  this.hideOptions = !this.hideOptions;
}

ToggleReOrderList(){
  this.toggleReOrderList = !this.toggleReOrderList;
  this.hideOptions = !this.hideOptions;
}

//TRANSPOSE METHODS
TransposeDown(transposedKey:string, apiid:string):void{
  let key:string = "";
  let keyIndex:number;
  let songIndex:number = this.listSongs.findIndex(x => x.apiid == apiid);
  if(transposedKey.includes("m")){
    key = transposedKey.substring(0,transposedKey.indexOf("m"));
    keyIndex = this.sharpKeys.indexOf(key)-1;
    if(keyIndex < 0){
      keyIndex += 12;
    }
    key = this.sharpKeys[keyIndex] + "m";
  }
  else{
    key = transposedKey;
    keyIndex = this.sharpKeys.indexOf(key)-1;
    if(keyIndex < 0){
      keyIndex += 12;
    }
    key = this.sharpKeys[keyIndex];
  }
  this.listSongs[songIndex].transposedKey = key;
}

TransposeUp(transposedKey:string, apiid:string):void{
  let key:string = "";
  let keyIndex:number;
  let songIndex:number = this.listSongs.findIndex(x => x.apiid == apiid);
  if(transposedKey.includes("m")){
    key = transposedKey.substring(0,transposedKey.indexOf("m"));
    keyIndex = this.sharpKeys.indexOf(key) +1;
    if(keyIndex > 11){
      keyIndex -= 12;
    }
    key = this.sharpKeys[keyIndex] + "m";
  }
  else{
    key = transposedKey;
    keyIndex = this.sharpKeys.indexOf(key)+1;
    if(keyIndex > 11){
      keyIndex -= 12;
    }
    key = this.sharpKeys[keyIndex];
  }
  this.listSongs[songIndex].transposedKey = key;
}

SaveTransposeChanges(){
  this.songService.SaveTransposeChanges(this.listSongs).subscribe((response:void)=>{
  });
  this.EnableTranspose();
}

DiscardChanges(){
  this.ngOnInit();
  this.EnableTranspose();
}

RestoreOriginalKeys(){
  this.listSongs.forEach((song) => {
    song.transposedKey = song.originalKey;
  });
}

//DRAG AND DROP FOR REORDING LIST
drop(event: CdkDragDrop<string[]>) {
  moveItemInArray(this.listSongs, event.previousIndex, event.currentIndex);
}

LogSongList(){
  console.log(this.listSongs);
}
}

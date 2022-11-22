import { Component, OnInit } from '@angular/core';
import { ApireturnsongarrayService } from '../apireturnsongarray.service';
import { NgForm } from '@angular/forms';
import { SongArray } from '../song-array';


@Component({
  selector: 'app-findsong',
  templateUrl: './findsong.component.html',
  styleUrls: ['./findsong.component.css']
})
export class FindsongComponent implements OnInit {

  songArray:SongArray = {} as SongArray;
  constructor(private apisongservice: ApireturnsongarrayService) { }

  ngOnInit(): void {

  }
  
  //uses name=searched song of searchSongForm, trims and formats string for API call
  searchForSong(form:NgForm):any{
    let song = form.form.value.searchedSong.trim().replaceAll(' ', '+');
    this.apisongservice.getSongArray(song).subscribe((response:any) => {
      console.log(response);
      this.songArray = response;
    });
  }
}

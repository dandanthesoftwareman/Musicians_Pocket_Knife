import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PlaylistService } from '../playlist.service';

@Component({
  selector: 'app-playlist-details',
  templateUrl: './playlist-details.component.html',
  styleUrls: ['./playlist-details.component.css']
})
export class PlaylistDetailsComponent implements OnInit {

  constructor(private http:HttpClient, private playlistService: PlaylistService) { }

  ngOnInit(): void {
  }

}

import { SocialAuthService } from '@abacritt/angularx-social-login';
import { Component, OnInit } from '@angular/core';
import { SongDetailService } from '../song-detail.service';

@Component({
  selector: 'app-song-details',
  templateUrl: './song-details.component.html',
  styleUrls: ['./song-details.component.css']
})
export class SongDetailsComponent implements OnInit {

  constructor(private authService:SocialAuthService, private songDetailService: SongDetailService) { }

  ngOnInit(): void {
    
  }

}

import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Secret } from 'src/secret';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http:HttpClient, @Inject("BASE_URL") private baseUrl:string) { }

  apiUrl:string = "https://api.getsongbpm.com/";
  search:string = "search/";
  song:string = "song/";
  
  getSongArray(song:string):any{
    return this.http.get(`${this.apiUrl}${this.search}?api_key=${Secret.apiKey}&type=song&lookup=${song}`);
  }

  getSongInfo(songID:string):any{
    return this.http.get(`${this.apiUrl}${this.song}?api_key=${Secret.apiKey}&id=${songID}`);
  }
}

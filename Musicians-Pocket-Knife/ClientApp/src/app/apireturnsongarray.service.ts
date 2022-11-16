import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Secret } from 'src/secret';

@Injectable({
  providedIn: 'root'
})
export class ApireturnsongarrayService {

  apiUrl:string = "https://api.getsongbpm.com/search/";
  constructor(private http:HttpClient, @Inject("BASE_URL") private baseUrl:string) { }

  getSongArray(song:string):any{
    return this.http.get(`${this.apiUrl}?api_key=${Secret.apiKey}&type=song&lookup=${song}`);
  }

}

import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Secret } from 'src/secret';

@Injectable({
  providedIn: 'root'
})
export class ApireturnsongarrayService {

  apiUrl:string = "https://api.getsongbpm.com/";
  search:string = "search/";
  constructor(private http:HttpClient, @Inject("BASE_URL") private baseUrl:string) { }

  getSongArray(song:string):any{
    return this.http.get(`${this.apiUrl}${this.search}?api_key=${Secret.apiKey}&type=song&lookup=${song}`);
  }

}

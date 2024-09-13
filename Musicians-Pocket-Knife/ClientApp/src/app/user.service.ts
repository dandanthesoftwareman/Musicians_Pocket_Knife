import { SocialUser } from '@abacritt/angularx-social-login';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  static user:SocialUser;
  endpoint:string = "api/Users";

  constructor(private http:HttpClient, @Inject("BASE_URL") private baseUrl:string) { }
  CreateNewUser(user:SocialUser):any{
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(user)
    return this.http.post(`${this.baseUrl}${this.endpoint}/CreateNewUser`, body, {'headers': headers})
  }
}

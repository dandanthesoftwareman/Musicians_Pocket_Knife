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

  CreateNewUser(id:string):any{
    return this.http.post(`${this.baseUrl}${this.endpoint}/CreateNewUser?googleId=${id}&name=${UserService.user.firstName}_${UserService.user.lastName}`, {})
  }
}

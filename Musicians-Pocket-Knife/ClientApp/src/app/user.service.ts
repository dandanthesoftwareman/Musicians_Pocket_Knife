import { SocialUser } from '@abacritt/angularx-social-login';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { NewUserRequest } from './interfaces/new-user-request';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  static user:SocialUser;
  endpoint:string = "api/Users";

  constructor(private http:HttpClient, @Inject("BASE_URL") private baseUrl:string) { }
  CreateNewUser(user: SocialUser): any {
    // Map SocialUser to NewUserRequest
    const newUserRequest: NewUserRequest = {
      id: user.id,
      email: user.email,
      photoUrl: user.photoUrl,
      firstName: user.firstName,
      lastName: user.lastName
    };
  
    // Send the mapped object without the extra fields like idToken, authToken, etc.
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(newUserRequest);
    return this.http.post(`${this.baseUrl}${this.endpoint}/CreateNewUser`, body, { headers });
  }
}

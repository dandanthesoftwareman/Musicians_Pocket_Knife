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

  VerifyExistingUser(userId: string): any {
    const verifyExistingUserRequest = {
      id: userId,
      active: UserService.user != null
    };
    
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(verifyExistingUserRequest);
    return this.http.post(`${this.baseUrl}${this.endpoint}/VerifyExistingUser`, body, { headers });
  }

  CreateNewUser(user: SocialUser): any {
    const newUserRequest: NewUserRequest = {
      id: user.id,
      email: user.email,
      firstName: user.firstName,
      lastName: user.lastName
    };

    // Send the mapped object without the extra fields like idToken, authToken, etc.
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(newUserRequest);
    return this.http.post(`${this.baseUrl}${this.endpoint}/CreateNewUser`, body, { headers });
  }
}

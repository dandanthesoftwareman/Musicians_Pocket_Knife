import { GoogleLoginProvider, SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { Component } from '@angular/core';
import { UserService } from '../user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  user: SocialUser = {} as SocialUser;
  loggedIn: boolean = false;

  constructor(private authService: SocialAuthService, private userService: UserService, private router: Router) { }
  ngOnInit(): void {

    this.authService.authState.subscribe((user) => {
      this.user = user;
      UserService.user = user;
      this.loggedIn = (user != null);
      if(this.loggedIn == true){
        this.userService.VerifyExistingUser(user.id).subscribe((response: boolean | null) => {
          if(response === true){
            if (this.loggedIn) {
              this.router.navigate(['/Playlists']);
            }
          }
          else if (response === false){
            this.userService.CreateNewUser(user).subscribe((response: any) => {
              // create new user, do nothing with response now, clean up user response later
            })
          }
          else {
            // null response, create popup that error in verifying user please signing in again try again
          }
        })
      }
    });
  }
  
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  
  signOut(): void {
    this.authService.signOut();
  }
}

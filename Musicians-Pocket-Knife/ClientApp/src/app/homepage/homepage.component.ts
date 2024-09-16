import { Router } from '@angular/router';
import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})

export class HomepageComponent implements OnInit {


  user: SocialUser = {} as SocialUser;
  loggedIn: boolean = false;
  constructor(private authService: SocialAuthService, private router: Router, private userService: UserService) { }

  ngOnInit(): void {
    this.authService.authState.subscribe((user) => {
      this.user = user;
      UserService.user = user;
      this.loggedIn = (user != null);
      if(this.loggedIn == true){
        this.userService.VerifyExistingUser(user.id).subscribe((response: boolean | null) => {
          if (response === true){
            if (this.loggedIn) {
              this.router.navigate(['/Playlists']);
            }
          }
          else if (response === false){
            this.userService.CreateNewUser(user).subscribe((response: any) => {
              // do nothing now, clean up/make use of user response later
            })
          }
          else {
            // create popup that error in verifying user, possibly reject login/restart login process
          }
        })
      }
    });
  }
}

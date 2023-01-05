import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from '@abacritt/angularx-social-login';
import { Secret } from 'src/secret';
import { HomepageComponent } from './homepage/homepage.component';
import { FindsongComponent } from './findsong/findsong.component';
import { SongDetailsComponent } from './song-details/song-details.component';
import { PlaylistsComponent } from './playlists/playlists.component';
import { PlaylistDetailsComponent } from './playlist-details/playlist-details.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    HomepageComponent,
    FindsongComponent,
    SongDetailsComponent,
    PlaylistsComponent,
    PlaylistDetailsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    SocialLoginModule,
    RouterModule.forRoot([
      { path: '', component: HomepageComponent, pathMatch: 'full' },
      { path: 'Findsong', component: FindsongComponent},
      { path: 'SongDetails/:id', component: SongDetailsComponent},
      { path: 'Playlists', component: PlaylistsComponent},
      { path: 'PlaylistDetails/:id', component: PlaylistDetailsComponent},
    ])
  ],
  providers: [
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              Secret.secret
            )
          }
        ]
      } as SocialAuthServiceConfig,
    }  
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

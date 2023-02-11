import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from '@abacritt/angularx-social-login';
import { Secret } from 'src/secret';
import { HomepageComponent } from './homepage/homepage.component';
import { FindsongComponent } from './findsong/findsong.component';
import { PlaylistsComponent } from './playlists/playlists.component';
import { PlaylistDetailsComponent } from './playlist-details/playlist-details.component';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AlertModule } from '@coreui/angular';
import { IconModule, IconSetService } from '@coreui/icons-angular';
import { CollapseModule } from '@coreui/angular';
import { AccordionModule, SharedModule } from '@coreui/angular';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    HomepageComponent,
    FindsongComponent,
    PlaylistsComponent,
    PlaylistDetailsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserModule,
    HttpClientModule,
    FormsModule,
    SocialLoginModule,
    DragDropModule,
    MatIconModule,
    AlertModule,
    AccordionModule,
    SharedModule,
    CollapseModule,
    IconModule,
    MatButtonModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: '', component: HomepageComponent, pathMatch: 'full' },
      { path: 'Findsong', component: FindsongComponent},
      { path: 'Playlists', component: PlaylistsComponent},
      { path: 'PlaylistDetails/:listId', component: PlaylistDetailsComponent}
    ]),
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
    },
    IconSetService  
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

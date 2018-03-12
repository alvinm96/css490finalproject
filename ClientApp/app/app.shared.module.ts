import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap/modal';
import { Ng2Webstorage } from 'ngx-webstorage';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { CallbackComponent } from './components/callback/callback.component';
import { CodeVerificationComponent } from './components/code-verification/code-verification.component';
import { UserComponent } from './components/user/user.component';
import { GroupsComponent } from './components/groups/groups.component';
import { ImagesComponent } from './components/images/images.component';
import { WelcomeComponent } from './components/welcome/welcome.component';
import { SearchComponent } from './components/search/search.component';
import { ResultsComponent } from './components/results/results.component';
import { ImageComponent } from './components/image/image.component';

import { ResultsService } from './services/results.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    RegisterComponent,
    CallbackComponent,
    CodeVerificationComponent,
    UserComponent,
    GroupsComponent,
    ImagesComponent,
    WelcomeComponent,
    SearchComponent,
    ResultsComponent,
    ImageComponent
  ],
  imports: [
    CommonModule,
    HttpModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: WelcomeComponent },
      { path: 'home', component: HomeComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'callback', component: CallbackComponent },
      { path: 'code-verification', component: CodeVerificationComponent },
      { path: 'u/:userId', component: UserComponent },
      { path: 'u/:userId/images', component: ImagesComponent },
      { path: 'u/:userId/groups', component: GroupsComponent },
      { path: 'search', component: SearchComponent },
      { path: 'results', component: ResultsComponent },
      { path: 'i/:imageId', component: ImageComponent },
      { path: '**', redirectTo: 'home' }
    ]),
    ModalModule.forRoot(),
    Ng2Webstorage
  ],
  providers: [
    ResultsService
  ]
})
export class AppModuleShared {
}

import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";

import {RouterModule} from '@angular/router';
import {AppComponent} from './app.component';

import {FormsModule} from '@angular/forms';
import {HttpClientModule} from "@angular/common/http";
// Components
import {UsersComponent} from './components/users/users.component';
import {UserDetailComponent} from './components/user-detail/user-detail.component';
import {HomeComponent} from './components/home/home.component';
import {UserDetailGuard} from "./guards/user-detail.guard";
import {NewUserComponent} from './components/new-user/new-user.component';
import {PostComponent} from './components/post/post.component';
import {PostListComponent} from './components/post-list/post-list.component';

const routes = [
  {path: 'users', component: UsersComponent},
  {path: 'users/:id/edit', canActivate: [UserDetailGuard], component: UserDetailComponent},
  {path: 'users/new', component: NewUserComponent},
  {path: 'posts', component: PostListComponent},
  {path: 'posts/:id', component: PostComponent},
  {path: 'home', component: HomeComponent},
  {path: '', redirectTo: 'home', pathMatch: 'full'},
  {path: '**', redirectTo: 'home', pathMatch: 'full'},
];

@NgModule({
  declarations: [
    AppComponent,
    UsersComponent,
    UserDetailComponent,
    HomeComponent,
    NewUserComponent,
    PostComponent,
    PostListComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(routes),
    FormsModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}

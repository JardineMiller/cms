import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {animate, state, style, transition, trigger} from "@angular/animations";
import {IUser} from "../../models/interfaces/user";

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
  animations: [
    trigger('fadeInOut', [
      state('void', style({
        opacity: 0
      })),
      transition('void <=> *', animate(200)),
    ]),
  ]
})
export class UsersComponent implements OnInit {

  constructor(private _http: HttpClient) {
  }

  private filter(filterString: string) {
    return this.users.filter((each) => {
      return each.name.indexOf(filterString) > -1 || each.email.indexOf(filterString) > -1;
    })
  }

  pageTitle: string = "Users";
  users: IUser[] = [];
  userKeys: string[] = [];
  filteredUsers: IUser[] = [];

  _filterString: string;
  get filterString(): string {
    return this._filterString;
  }
  set filterString(value: string) {
    this._filterString = value;
    this.filteredUsers = this.filterString ? this.filter(this._filterString) : this.users;
  }

  ngOnInit(): void {
    //TODO: This should live in a users service
    this._http.get('api/users').subscribe(values => {
      this.users = values as any[];
      this.filteredUsers = this.users;
      if (this.users.length) {
        this.userKeys = Object.keys(this.users[0]);
      }
    })
  }

  addUser() {
    let lastId = this.users[this.users.length - 1].id;
    this.users.push({id: lastId + 1, name: "User", email: "email@provider.com"});
  }

  delete() {
    this.users.pop();
  }
}

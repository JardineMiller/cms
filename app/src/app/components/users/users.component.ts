import {Component, OnInit} from '@angular/core';
import {animate, state, style, transition, trigger} from "@angular/animations";
import {IUser} from "../../../models/interfaces/user";
import {UsersService} from "../../services/users.service";

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

  private _userService: UsersService;

  public pageTitle: string = "Users";
  public users: IUser[] = [];
  public filteredUsers: IUser[] = [];
  public keys: string[];

  constructor(private userService: UsersService) {
    this._userService = userService;
  }

  private filter(filterString: string) {
    return this.users.filter((each) => {
      return each.name.indexOf(filterString) > -1 || each.email.indexOf(filterString) > -1;
    })
  }

  _filterString: string;
  get filterString(): string {
    return this._filterString;
  }
  set filterString(value: string) {
    this._filterString = value;
    this.filteredUsers = this.filterString ? this.filter(this._filterString) : this.users;
  }

  ngOnInit(): void {
    this._userService.init().then(
      res => {
        this.users = this.userService.getAll();
        this.filteredUsers = this.users;
        this.keys = this.users.length ? Object.keys(this.users[0]) : [];
      }
    )
  }

  addUser():void {

  }

  delete():void {

  }
}
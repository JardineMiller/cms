import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  constructor(private _http: HttpClient) {

  }

  users: any[] = [];
  userKeys: string[] = [];
  pageTitle: string = "Users";

  ngOnInit(): void {
    //TODO: This should live in a users service
    this._http.get('api/users').subscribe(values => {
      this.users = values as any[];
      if (this.users.length) {
        this.userKeys = Object.keys(this.users[0]);
      }
    })
  }
}

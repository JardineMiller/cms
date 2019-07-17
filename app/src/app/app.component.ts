import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(private _http: HttpClient) {

  }

  users: any[] = [];
  userKeys: string[] = [];

  ngOnInit(): void {
    this._http.get('api/users').subscribe(values => {
      this.users = values as any[];
      if (this.users.length) {
        this.userKeys = Object.keys(this.users[0]);
      }
    })
  }
}

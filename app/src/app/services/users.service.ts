import {Injectable} from '@angular/core';
import {IUser} from "../../models/interfaces/user";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {map, publishReplay, refCount} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  private baseUrl: string;
  public users: Observable<IUser[]>;

  constructor(private http: HttpClient) {
    this.baseUrl = "api/users";
  }

  public getUsers(): Observable<IUser[]> {
    if (!this.users) {
      this.users = this.http.get(this.baseUrl).pipe(
        map(data => data as IUser[]),
        publishReplay(1),
        refCount()
      )
    }

    return this.users;
  }
}

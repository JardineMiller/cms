import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject, Observable} from "rxjs";
import {IUser} from "../../models/interfaces/user";

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  public users: Observable<IUser[]>;
  private readonly baseUrl: string;
  private _users: BehaviorSubject<IUser[]>;
  private store: {
    users: IUser[]
  };

  constructor(private http: HttpClient) {
    this.baseUrl = "api/users";
    this.store = {users: []};
    this._users = <BehaviorSubject<IUser[]>>new BehaviorSubject([]);
    this.users = this._users.asObservable();
  }

  public create(users: IUser[]) {
    this.http.post<IUser[]>(this.baseUrl, JSON.stringify(users)).subscribe(
      data => {
        data.forEach((item) => {
          this.store.users.push(item);
        });
        this._users.next(Object.assign({}, this.store).users);
      },
      error => {
        console.log('Could not create users.')
      });
  }

  public update(users: IUser[]) {
    this.http.put<IUser[]>(this.baseUrl, JSON.stringify(users)).subscribe(
      data => {
        data.forEach((item) => {
          this.store.users.forEach((user, u) => {
            if (user.id == item.id) {
              this.store.users[u] = item;
            }
          })
        });

        this._users.next(Object.assign({}, this.store).users);
      },
      error => {
        console.log('Could not update users');
      }
    )
  }

  public remove(userIds: number[]) {
    this.http.delete<number[]>(this.baseUrl).subscribe(
      response => {
        response.forEach((deleteId) => {
          this.store.users.forEach((user, u) => {
            if (user.id == deleteId) {
              this.store.users.splice(u, 1);
            }
          })
        });

        this._users.next(Object.assign({}, this.store).users);
      },
      error => {
        console.log('Could not delete users.')
      }
    );
  }

  public load(id: number) {
    const url = `${this.baseUrl}/${id}`;
    this.http.get<IUser>(url).subscribe(
      data => {
        let notFound = true;

        this.store.users.forEach((item, index) => {
          if (item.id == data.id) {
            this.store.users[index] = data;
            notFound = false;
          }
        });

        if (notFound) {
          this.store.users.push(data)
        }

        this._users.next(Object.assign({}, this.store).users);
      },
      error => {
        console.log(`Could not load user [${id}]: ${error}`);

      }
    )
  }

  public loadAll() {
    this.http.get<IUser[]>(this.baseUrl).subscribe(
      data => {
        this.store.users = data as IUser[];
        this._users.next(Object.assign({}, this.store).users);
      },
      error => {
        console.log(`Could not load users: ${error}`);
      }
    )
  }

}

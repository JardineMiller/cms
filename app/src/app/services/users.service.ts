import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IUser} from "../../models/interfaces/user";

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  private readonly baseUrl: string;
  private store: {
    users: Map<number, IUser>
  };

  constructor(private http: HttpClient) {
    this.baseUrl = "api/users";
    this.store = {
      users: new Map()
    };
  }

  public getAll() {
    //TODO: Figure out a way to sensibly cache this. It's expensive to turn the whole map into a list every time anyone needs it.
    return Array.from(this.store.users.values());
  }

  public get(id: number) {
    return this.store.users.get(id);
  }

  public create(users: IUser[]) {
    return new Promise((resolve, reject) => {
      this.http.post<IUser[]>(this.baseUrl, JSON.stringify(users)).toPromise().then(
        newUsers => {
          newUsers.forEach(u => this.store.users.set(u.id, u));
          resolve();
        },
        err => {
          console.log('Could not create users');
          reject(err);
        }
      )
    })
  }

  public update(users: IUser[]) {
    return new Promise((resolve, reject) => {
      this.http.put<IUser[]>(this.baseUrl, JSON.stringify(users)).toPromise().then(
        updatedUsers => {
          updatedUsers.forEach(u => this.store.users.set(u.id, u));
          resolve();
        },
        err => {
          console.log('Could not update users');
          reject(err);

        }
      )
    })
  }

  public remove(userIds: number[]) {
    return new Promise((resolve, reject) => {
      this.http.delete<number[]>(this.baseUrl).toPromise().then(
        deletedIds => {
          deletedIds.forEach(id => this.store.users.delete(id));
          resolve();
        },
        err => {
          console.log('Could not delete users.')
          reject(err);
        }
      )
    })
  }

  public load(id: number) {
    const url = `${this.baseUrl}/${id}`;
    return new Promise((resolve, reject) => {
      this.http.get<IUser>(url).toPromise().then(
        user => {
          this.store.users.set(user.id, user);
          resolve();
        },
        err => {
          console.log(`Could not load user [${id}]: ${err}`);
          reject(err);
        }
      )
    })
  }

  public init() {
    return new Promise((resolve, reject) => {
      this.http.get<IUser[]>(this.baseUrl).toPromise().then(
        data => {
          data.forEach(u => this.store.users.set(u.id, u));
          resolve();
        },
        reject => {
          console.log(`Could not load users: ${reject}`);
          reject(reject);
        }
      )
    });
  }
}
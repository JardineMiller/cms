import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PostsService {

  // private readonly baseUrl: string;
  // private store: {
  //   postsByUserId: Map<number, IPost[]>,
  //   postsById: Map<number, IPost>
  // };
  //
  // constructor(private http: HttpClient) {
  //   this.baseUrl = "api/posts";
  //   this.store = {
  //     postsByUserId: new Map(),
  //     postsById: new Map()
  //   };
  // }
  //
  // public getAll() {
  //   //TODO: Figure out a way to sensibly cache this. It's expensive to turn the whole map into a list every time anyone needs it.
  //   return Array.from(this.store.users.values());
  // }
  //
  // public get(id: number) {
  //   let user = this.store.users.get(id);
  //
  //   if (!user) {
  //     this.fetch(id).then(res => {
  //       user = this.store.users.get(id);
  //     });
  //   }
  //
  //   return user;
  // }
  //
  // public create(users: IPost) {
  //   return new Promise((resolve, reject) => {
  //     this.http.post<IPost[]>(this.baseUrl, JSON.stringify(users)).toPromise().then(
  //       newUsers => {
  //         newUsers.forEach(u => this.store.users.set(u.id, u));
  //         resolve();
  //       },
  //       err => {
  //         console.log('Could not create users');
  //         reject(err);
  //       }
  //     )
  //   })
  // }
  //
  // public update(users: IPost) {
  //   return new Promise((resolve, reject) => {
  //     this.http.put<IPost[]>(this.baseUrl, JSON.stringify(users)).toPromise().then(
  //       updatedUsers => {
  //         updatedUsers.forEach(u => this.store.users.set(u.id, u));
  //         resolve();
  //       },
  //       err => {
  //         console.log('Could not update users');
  //         reject(err);
  //
  //       }
  //     )
  //   })
  // }
  //
  // public remove(userIds: number[]) {
  //   return new Promise((resolve, reject) => {
  //     this.http.delete<number[]>(this.baseUrl).toPromise().then(
  //       deletedIds => {
  //         deletedIds.forEach(id => this.store.users.delete(id));
  //         resolve();
  //       },
  //       err => {
  //         console.log('Could not delete users.')
  //         reject(err);
  //       }
  //     )
  //   })
  // }
  //
  // public fetch(id: number) {
  //   const url = `${this.baseUrl}/${id}`;
  //   return new Promise((resolve, reject) => {
  //     this.http.get<IPost>(url).toPromise().then(
  //       user => {
  //         this.store.users.set(user.id, user);
  //         resolve();
  //       },
  //       err => {
  //         console.log(`Could not load user [${id}]: ${err}`);
  //         reject(err);
  //       }
  //     )
  //   })
  // }
  //
  // public init() {
  //   return new Promise((resolve, reject) => {
  //     this.http.get<IPost[]>(this.baseUrl).toPromise().then(
  //       data => {
  //         data.forEach(u => this.store.users.set(u.id, u));
  //         resolve();
  //       },
  //       reject => {
  //         console.log(`Could not load users: ${reject}`);
  //         reject(reject);
  //       }
  //     )
  //   });
  // }
}

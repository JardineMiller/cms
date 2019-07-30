import {Injectable} from '@angular/core';
import {IPost} from "../../../models/interfaces/post";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class PostsService {

  private readonly baseUrl: string;
  public initialised: boolean = false;
  private store: {
    postsByUserId: Map<number, IPost[]>,
    postsById: Map<number, IPost>
  };

  constructor(private http: HttpClient) {
    this.baseUrl = "api/posts";
    this.store = {
      postsByUserId: new Map(),
      postsById: new Map()
    };
  }

  public getAll(): IPost[] {
    //TODO: Figure out a way to sensibly cache this. It's expensive to turn the whole map into a list every time anyone needs it.
    return Array.from(this.store.postsById.values());
  }

  public getById(postId: number): IPost {
    let post = this.store.postsById.get(postId);

    if (!post) {
      this.fetch(postId).then(res => {
        post = this.store.postsById.get(postId);
      });
    }

    return post;
  }

  public getByUserId(userId: number): IPost[] {
    let posts = this.store.postsByUserId.get(userId);

    if (!posts) {
      this.fetch(userId).then(res => {
        posts = this.store.postsByUserId.get(userId);
      });
    }

    return posts;
  }

  public create(userId: number, post: IPost) {
    let url = `${this.baseUrl}/user/${userId}`;

    return new Promise((resolve, reject) => {
        this.http.post<IPost>(url, post).toPromise().then(
          newPost => {
            this.store.postsByUserId.set(newPost.authorId, [newPost]);
            this.store.postsById.set(newPost.id, newPost);
            resolve(newPost);
          },
          err => {
            console.log('Could not create post');
            reject(err);
          });
      }
    )
  }

  public update(userId: number, post: IPost) {
    let url = `${this.baseUrl}/user/${userId}`;

    return new Promise((resolve, reject) => {
        this.http.put<IPost>(url, post).toPromise().then(
          updatedPost => {
            this.store.postsById.set(updatedPost.id, updatedPost);

            let userPosts = this.store.postsByUserId.get(updatedPost.authorId);
            userPosts.forEach((post, index) => {
              if (post.id === updatedPost.id) {
                userPosts[index] = updatedPost;
              }
            });

            resolve(updatedPost);
          },
          err => {
            console.log('Could not create post');
            reject(err);
          });
      }
    )
  }

  public delete(userId: number, postId: number) {
    let url = `${this.baseUrl}/user/${userId}?postId=${postId}`;

    return new Promise((resolve, reject) => {
        this.http.delete<number>(url).toPromise().then(
          postId => {
            let post = this.store.postsById.get(postId);
            this.store.postsById.delete(postId);

            let userPosts = this.store.postsByUserId.get(post.authorId);
            userPosts.forEach((post, index) => {
              if (post.id === postId) {
                userPosts.splice(index, 1);
              }
            });

            resolve(postId);
          },
          err => {
            console.log('Could not create post');
            reject(err);
          });
      }
    )
  }

  public fetch(postId: number) {
    const url = `${this.baseUrl}/${postId}`;
    return new Promise((resolve, reject) => {
      this.http.get<IPost>(url).toPromise().then(
        post => {
          this.store.postsById.set(post.id, post);
          let userPosts = this.store.postsByUserId.get(post.authorId);

          if (userPosts) {
            userPosts.push(post);
          } else {
            this.store.postsByUserId.set(post.authorId, [post])
          }

          resolve(post);
        },
        err => {
          console.log(`Could not load user [${postId}]: ${err}`);
          reject(err);
        }
      )
    })
  }

  public init() {
    return new Promise((resolve, reject) => {
      this.http.get<IPost[]>(this.baseUrl).toPromise().then(
        posts => {
          posts.forEach(post => {
            this.store.postsById.set(post.id, post);
            let userPosts = this.store.postsByUserId.get(post.id);

            if (userPosts) {
              userPosts.push(post);
            } else {
              this.store.postsByUserId.set(post.authorId, [post])
            }

          });
          this.initialised = true;
          resolve(posts);
        },
        reject => {
          console.log(`Could not load users: ${reject}`);
          reject(reject);
        }
      )
    });
  }
}

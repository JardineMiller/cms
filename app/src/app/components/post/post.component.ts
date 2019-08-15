import {Component, OnInit} from '@angular/core';
import {PostsService} from "../../services/posts/posts.service";
import {ActivatedRoute, Router} from "@angular/router";
import {IPost} from "../../../models/interfaces/post";
import {IComment} from "../../../models/interfaces/IComment";

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss']
})
export class PostComponent implements OnInit {
  private post: IPost;
  public editMode: boolean;
  public newComment: IComment;

  constructor(private posts: PostsService, private route: ActivatedRoute, private router: Router) {
    this.editMode = false;
    this.newComment = {content: "", authorId: 1};
  }

  ngOnInit() {
    let postId = parseInt(this.route.snapshot.paramMap.get('id'));
    if (this.posts.initialised) {
      this.post = this.posts.getById(postId);
      return;
    }

    this.posts.init().then(res => {
      this.post = this.posts.getById(postId);
    })
  }

  private toggleEditMode() : void {
    this.editMode = !this.editMode;
  }

  private save() : void {
    this.posts.update(this.post.authorId, this.post).then(updatedPost => {
      this.post = updatedPost as IPost;
      this.editMode = false;
    })
  }

  private deletePost(postId: number) {
    this.posts.delete(this.post.authorId, this.post.id).then(res => {
      this.router.navigate(['/posts']);
    })
  }

  private saveComment() {
    this.post.comments.push(this.newComment);
    this.save();
  }
}

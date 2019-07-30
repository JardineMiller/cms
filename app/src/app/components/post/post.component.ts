import {Component, OnInit} from '@angular/core';
import {PostsService} from "../../services/posts/posts.service";
import {ActivatedRoute, Router} from "@angular/router";
import {IPost} from "../../../models/interfaces/post";

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss']
})
export class PostComponent implements OnInit {
  private post: IPost;
  public editMode: boolean;

  constructor(private posts: PostsService, private route: ActivatedRoute, private router: Router) {
    this.editMode = false;
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
}

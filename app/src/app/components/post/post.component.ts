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

  constructor(private posts: PostsService, private route: ActivatedRoute, private router: Router) {

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

}

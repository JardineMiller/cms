import {Component, OnInit} from '@angular/core';
import {PostsService} from "../../services/posts/posts.service";
import {IPost} from "../../../models/interfaces/post";

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.scss']
})
export class PostListComponent implements OnInit {

  public posts: IPost[];

  constructor(private postService: PostsService) {

  }

  ngOnInit() {
    this.postService.init().then(
      res => {
        this.posts = this.postService.getAll();
        console.table(this.posts);
      }
    )
  }

}

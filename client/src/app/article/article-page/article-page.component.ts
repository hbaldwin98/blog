import { UserComment } from 'src/app/_models/usercomment';
import { CommentService } from './../../_services/comment.service';
import { PostComment } from './../../_models/postComment';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, ExtraOptions } from '@angular/router';
import { Article } from 'src/app/_models/article';
import { ViewportScroller } from '@angular/common';

@Component({
  selector: 'app-article-page',
  templateUrl: './article-page.component.html',
  styleUrls: ['./article-page.component.css']
})
export class ArticlePageComponent implements OnInit {
  @Input() article!: Article;
  comments!: UserComment[];
  commentForm!: FormGroup;
  newComment!: PostComment;

  constructor(private route: ActivatedRoute, private fb: FormBuilder, private commentService: CommentService, private viewportScroller: ViewportScroller) { }

  ngOnInit(): void {
    this.initializeForm();
    this.route.data.subscribe((data => {
      this.article = data.article;
      this.comments = data.article.comments.sort((a: UserComment, b: UserComment) => new Date(b.dateCommented).getTime() - new Date(a.dateCommented).getTime());
    }))
    // ? scrolls the page down for the user if the the route contains a comments fragment
    // ? this only works with a setTimeout, why?
    // TODO: find better alternative if possible for scrolling
    this.route.fragment.subscribe(x => {
      if (x === "comments") {
        setTimeout(() => {
          this.scrollToComments();
        }, 200);
      }
    });
  }

  // TODO: Make form actually use form builder. This is mostly redundant as of now.
  initializeForm() {
    this.commentForm = this.fb.group({
      commenterName: ['', Validators.required],
      content: ['', Validators.required],
    })
  }

  /**
   * TODO: This whole process needs to be changed. Comments should not be loaded until the page has been accessed.
   * TODO: They should be in their own service and updated accordingly everytime the page is accessed as new comments
   * TODO: could be sent after leaving a page. This sort of works now, however, for testing purposes.
   * @memberof ArticlePageComponent
   */
  postComment() {
    this.newComment = {
      commenterName: this.commentForm.value['commenterName'],
      contents: this.commentForm.value['content']
    }

    if (this.commentService.postComment(this.newComment, this.article.urlIdentity)) {
      let comment: UserComment = {
        commenterName: this.newComment.commenterName,
        contents: this.newComment.contents,
        dateCommented: new Date(Date.now())
      }

      this.comments.push(comment)
      this.comments.sort((a: UserComment, b: UserComment) => new Date(b.dateCommented).getTime() - new Date(a.dateCommented).getTime());
    }
  }

  scrollToComments() {
    this.viewportScroller.scrollToAnchor('comments');
  }

}

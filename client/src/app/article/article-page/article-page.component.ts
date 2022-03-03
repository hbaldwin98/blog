import { ArticlesService } from 'src/app/_services/articles.service';
import { AccountService } from './../../_services/account.service';
import { UserComment } from 'src/app/_models/usercomment';
import { CommentService } from './../../_services/comment.service';
import { PostComment } from './../../_models/postComment';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, ExtraOptions, Router } from '@angular/router';
import { Article } from 'src/app/_models/article';
import { ViewportScroller } from '@angular/common';
import { map } from 'rxjs/operators';

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

  constructor(private route: ActivatedRoute, private fb: FormBuilder, private commentService: CommentService,
      private viewportScroller: ViewportScroller, public accountService: AccountService,
      private articleService: ArticlesService, private router: Router) { }

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
    // send to comment to db and retrieve response containing the commment + it's db id
    // map that id to a new comment that we push to the user's view
    this.commentService.postComment(this.newComment, this.article.urlIdentity).pipe(map((comment: any) => {
      let userComment: UserComment = {
        commenterName: this.newComment.commenterName,
        id: comment.id,
        contents: this.newComment.contents,
        dateCommented: new Date(Date.now())
      }

      this.comments.push(userComment)
      this.comments.sort((a: UserComment, b: UserComment) => new Date(b.dateCommented).getTime() - new Date(a.dateCommented).getTime());
    })).subscribe();

  }

  deleteComment(id: number) {
    this.commentService.deleteComment(id);
    this.comments = this.comments.filter(c => c.id !== id);
  }

  deleteArticle() {
    this.articleService.deleteArticle(this.article.urlIdentity);
    this.router.navigateByUrl('/');
  }

  scrollToComments() {
    this.viewportScroller.scrollToAnchor('comments');
  }

}

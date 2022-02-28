import { CommentService } from './../../_services/comment.service';
import { PostComment } from './../../_models/postComment';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Article } from 'src/app/_models/article';

@Component({
  selector: 'app-article-page',
  templateUrl: './article-page.component.html',
  styleUrls: ['./article-page.component.css']
})
export class ArticlePageComponent implements OnInit {
  @Input() article!: Article;
  commentForm!: FormGroup;
  newComment!: PostComment;

  constructor(private route: ActivatedRoute, private fb: FormBuilder, private commentService: CommentService) { }

  ngOnInit(): void {
    this.initializeForm();
    this.route.data.subscribe((data => {
      this.article = data.article;
    }))
  }

  initializeForm() {
    this.commentForm = this.fb.group({
      commenterName: ['', Validators.required],
      content: ['', Validators.required],
    })
  }

  postComment() {
    this.newComment = {
      commenterName: this.commentForm.value['commenterName'],
      contents: this.commentForm.value['content']
    }

    this.commentService.postComment(this.newComment, this.article.urlIdentity);
  }

}

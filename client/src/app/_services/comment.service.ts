import { environment } from './../../environments/environment';
import { PostComment } from './../_models/postComment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  postComment(comment: PostComment, title: string) {
    console.log(comment, title);

    return this.http.post(this.baseUrl + 'comments/post-comment/' + title, comment).subscribe(response => {
      return response;
    });
  }
}

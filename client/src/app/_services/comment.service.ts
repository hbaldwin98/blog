import { environment } from './../../environments/environment';
import { PostComment } from './../_models/postComment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  postComment(comment: PostComment, title: string) {
    return this.http.post(this.baseUrl + 'comments/post-comment/' + title, comment);
  }

  deleteComment(id: number) {
    return this.http.delete(this.baseUrl + 'comments/delete-comment/' + id, {}).subscribe(response => {
      return response;
    });
  }
}

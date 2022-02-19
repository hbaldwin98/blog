
import { Article } from './../_models/article';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ArticlesService {
  baseUrl = "https://localhost:5001/";

  constructor(private http: HttpClient) { }

  /**
   *  Retrieves the list of articles from the API.
   *
   * @yields {Observable} Article[]
   * @memberof ArticlesService
   */
  getArticles() {
    return this.http.get<Article[]>(this.baseUrl + 'Articles');
  }

  getArticle(title: string) {
    return this.http.get<Article>(this.baseUrl + 'Articles/' + title);
  }
}

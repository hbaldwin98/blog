import { CreateArticle } from './../_models/createArticle';
import { environment } from './../../environments/environment';
import { map, take } from 'rxjs/operators';
import { Article } from './../_models/article';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ArticlesService {
  private baseUrl = environment.apiUrl;
  articlesCache: Article[] = [];

  constructor(private http: HttpClient) { }

  /**
   *  Retrieves the list of articles from the API.
   *
   * @yields {Observable} Article[]
   * @memberof ArticlesService
   * TODO: Change retrieval of articles to just retrieve Title, Date, and Author. Currently Receiving all comments even when unnecessary.
   */
  getArticles() {
    if (this.articlesCache.length > 0) return of(this.articlesCache);

    return this.http.get<Article[]>(this.baseUrl + 'Articles').pipe(map(response => {
      this.articlesCache = response;
      return response;
    }));
  }

  getArticle(urlIdentity: string) {
    const article = this.articlesCache.find(article => article.urlIdentity == urlIdentity);

    if (article) return of(article);

    return this.http.get<Article>(this.baseUrl + 'Articles/' + urlIdentity);
  }

  postArticle(article: CreateArticle) {
    return this.http.post(this.baseUrl + 'articles', article).pipe(map((response: Article | any) => {
      this.articlesCache.unshift(response);
    }))
  }

  deleteArticle(url: string) {
    return this.http.delete(this.baseUrl + 'articles/delete-article/' + url).subscribe(() => {
      this.articlesCache = this.articlesCache.filter(u => u.urlIdentity !== url);
    });
  }
}

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
  baseUrl = environment.apiUrl;
  articlesCache: Article[] = [];

  constructor(private http: HttpClient) { }

  /**
   *  Retrieves the list of articles from the API.
   *
   * @yields {Observable} Article[]
   * @memberof ArticlesService
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
}

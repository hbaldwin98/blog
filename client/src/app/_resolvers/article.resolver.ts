import { ArticlesService } from './../_services/articles.service';
import { Article } from 'src/app/_models/article';
import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ArticleResolver implements Resolve<Article> {

  constructor(private articleService: ArticlesService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<Article> {
    return this.articleService.getArticle(route.params.urlIdentity);
  }
}

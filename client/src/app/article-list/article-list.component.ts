import { ArticlesService } from './../_services/articles.service';
import { Component, OnInit } from '@angular/core';
import { Article } from '../_models/article';


@Component({
  selector: 'app-article-list',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.css']
})
export class ArticleListComponent implements OnInit {
  articles!: Article[];

  constructor(private articlesService: ArticlesService) { }

  ngOnInit(): void {
    this.getArticles();
  }

  getArticles() {
    this.articlesService.getArticles().subscribe(response => {
      this.articles = response;
    });
  }
}

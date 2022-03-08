import { Component, Input, OnInit } from '@angular/core';
import { Article } from 'src/app/_models/article';

@Component({
  selector: 'app-article-featured-card',
  templateUrl: './article-featured-card.component.html',
  styleUrls: ['./article-featured-card.component.css']
})
export class ArticleFeaturedCardComponent implements OnInit {
  @Input() article!: Article;
  month!: number;
  year!: number;
  constructor() { }

  ngOnInit(): void {
    this.article.dateCreated = new Date(this.article.dateCreated);
    this.month = this.article.dateCreated.getMonth() + 1;
  }

}

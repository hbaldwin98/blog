import { Component, Input, OnInit } from '@angular/core';
import { Article } from 'src/app/_models/article';


@Component({
  selector: 'app-article-card',
  templateUrl: './article-card.component.html',
  styleUrls: ['./article-card.component.css']
})
export class ArticleCardComponent implements OnInit {
  @Input() article!: Article;
  month!: number;
  year!: number;
  constructor() { }

  ngOnInit(): void {
    this.article.dateCreated = new Date(this.article.dateCreated);
    this.month = this.article.dateCreated.getMonth() + 1;
    console.log(this.month);
  }

}

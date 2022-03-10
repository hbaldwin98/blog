import { Router } from '@angular/router';
import { ArticlesService } from 'src/app/_services/articles.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-new-article',
  templateUrl: './new-article.component.html',
  styleUrls: ['./new-article.component.css']
})
export class NewArticleComponent implements OnInit {
  newArticle: any = {};
  editor: any;

  constructor(private articlesService: ArticlesService, private router: Router) { }

  ngOnInit(): void {
  }

  postArticle() {
    this.articlesService.postArticle(this.newArticle).subscribe(() => {this.router.navigateByUrl('/');});
  }

}

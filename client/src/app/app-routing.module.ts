import { NewArticleComponent } from './article/new-article/new-article.component';
import { ArticleListComponent } from './article/article-list/article-list.component';
import { HomeComponent } from './home/home.component';
import { ArticlePageComponent } from './article/article-page/article-page.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ArticleResolver } from './_resolvers/article.resolver';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'home', component: HomeComponent},
  {path: 'articles/:urlIdentity', component: ArticlePageComponent, resolve: {article: ArticleResolver}},
  {path: 'new-article', component: NewArticleComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

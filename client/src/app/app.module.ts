import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { NgxSpinner, NgxSpinnerModule } from 'ngx-spinner';
import { EditorModule } from '@tinymce/tinymce-angular';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ArticleCardComponent } from './article/article-card/article-card.component';
import { ArticleListComponent } from './article/article-list/article-list.component';
import { ArticlePageComponent } from './article/article-page/article-page.component';
import { NewArticleComponent } from './article/new-article/new-article.component';
import { LoginComponent } from './login/login.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { FooterComponent } from './footer/footer.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { ArticleFeaturedCardComponent } from './article/article-featured-card/article-featured-card.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SafeHtmlPipe } from './_pipes/safehtml.pipe';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    ArticleCardComponent,
    ArticleListComponent,
    ArticlePageComponent,
    NewArticleComponent,
    LoginComponent,
    NotFoundComponent,
    FooterComponent,
    ArticleFeaturedCardComponent,
    SafeHtmlPipe,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    EditorModule,
    NgxSpinnerModule
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
              {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi:true}],
  bootstrap: [AppComponent]
})
export class AppModule { }

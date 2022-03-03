import { AccountService } from './../_services/account.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};

  constructor(public accountService: AccountService, private routr: Router) { }

  ngOnInit(): void {
  }

  login() {
    this.accountService.login(this.model).subscribe((response) => {
      this.routr.navigateByUrl('/');
    });
  }


  logout() {
    this.accountService.logout();
  }

}

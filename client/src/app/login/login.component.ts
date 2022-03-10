import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
  loginForm!: FormGroup;
  validationError = false;
  constructor(public accountService: AccountService, private routr: Router, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  login() {
    if (this.loginForm.valid) {
      this.accountService.login(this.loginForm.value).subscribe((response) => {
        this.routr.navigateByUrl('/');
      }, () => {
        this.validationError = true;
      });
    }
  }


  logout() {
    this.accountService.logout();
  }
}

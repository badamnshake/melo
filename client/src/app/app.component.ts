import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export default class AppComponent implements OnInit {
  title = 'Melo';
  users: any;

  constructor(private http: HttpClient, private accountService: AccountService) {}
  // hello
  ngOnInit() {
    this.getUsers();
    this.setCurrentUser();
  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);

  };
  getUsers() {
    this.http.get('https://localhost:7020/api/users').subscribe(
      (response) => {
        this.users = response;
      },
      (error) => {}
    );
  }
}

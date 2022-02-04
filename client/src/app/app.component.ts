import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export default class AppComponent implements OnInit {
  title = 'Melo';
  users: any;

  constructor(private http: HttpClient) {}
  // hello
  ngOnInit() {
    this.getUsers();
  }
  getUsers() {
    this.http.get('https://localhost:7020/api/users').subscribe(
      (response) => {
        this.users = response;
      },
      (error) => {}
    );
  }
}

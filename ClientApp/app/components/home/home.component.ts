import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { SessionStorageService } from 'ngx-webstorage';
import { Router } from '@angular/router';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  recentPosts: any[];

  constructor(private http: Http,
              private sessionSt: SessionStorageService,
              private router: Router) { }

  ngOnInit() {
    if (this.sessionSt.retrieve('username') === null) {
      this.router.navigateByUrl('/');
    }

    this.http.get('/api/Images')
      .toPromise()
      .then((res) => {
        this.recentPosts = res.json().results;
      })
      .catch((err) => {
        console.log(err);
      });
  }

  getGroups() {

  }
}

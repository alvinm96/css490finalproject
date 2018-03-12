import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';
import { SessionStorageService } from 'ngx-webstorage';

@Component({
  selector: 'search',
  templateUrl: './search.component.html'
})
export class SearchComponent implements OnInit {
  filters: number[] = [];

  constructor(private sessionSt: SessionStorageService,
              private router: Router,
              private http: Http) { }

  ngOnInit() {
    if (this.sessionSt.retrieve('username') === null) {
      this.router.navigateByUrl('/');
    }
  }

  search(form: any) {
    let params = new URLSearchParams();
    params.append('imageName', form.value.imageName);

    if (form.value.groupName) {
      params.append('groupName', form.value.groupName);
    } else if (form.value.userName) {
      params.append('userName', form.value.userName);
    }

    this.http.get('/api/Images', { params: params })
      .toPromise()
      .then((res) => {
        console.log(res);
      })
      .catch((err) => {
        console.log(err);
      });
  }
}

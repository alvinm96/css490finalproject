import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';
import { SessionStorageService } from 'ngx-webstorage';

import { ResultsService } from '../../services/results.service';

@Component({
  selector: 'search',
  templateUrl: './search.component.html'
})
export class SearchComponent implements OnInit {
  filters: number[] = [];

  constructor(private sessionSt: SessionStorageService,
              private router: Router,
              private http: Http,
              private resultsService: ResultsService) { }

  ngOnInit() {
    if (this.sessionSt.retrieve('username') === null) {
      this.router.navigateByUrl('/');
    }
  }

  search(form: any) {
    let params = new URLSearchParams();

    if (form.value.groupName) {
      params.append('groupName', form.value.groupName);
    } else if (form.value.userName) {
      params.append('userName', form.value.userName);
    } else if (form.value.imageName) {
      params.append('imageName', form.value.imageName);
    }

    this.http.get('/api/Images', { params: params })
      .toPromise()
      .then((res) => {
        for (var i = 0; i < res.json().results.length; i++) {
          this.resultsService.addResult(res.json().results[i]);
        }

        this.router.navigateByUrl('/results');
      })
      .catch((err) => {
        console.log(err);
      });
  }
}

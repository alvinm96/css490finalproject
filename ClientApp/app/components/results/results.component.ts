import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { SessionStorageService } from 'ngx-webstorage';

import { ResultsService } from '../../services/results.service';

@Component({
  selector: 'results',
  templateUrl: './results.component.html',
  styles: ['.images { max-width: 25vh } ']
})

export class ResultsComponent implements OnInit, OnDestroy {
  results: any;

  constructor(private router: Router,
              private resultsService: ResultsService,
              private sessionSt: SessionStorageService) { }

  ngOnInit() {
    if (this.sessionSt.retrieve('username') === null) {
      this.router.navigateByUrl('/');
    }

    this.results = this.resultsService.results;
  }

  ngOnDestroy() {
    this.resultsService.resetResults();
  }
}
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SessionStorageService } from 'ngx-webstorage';


@Component({
  selector: 'images',
  templateUrl: './images.component.html'
})

export class ImagesComponent implements OnInit {
  constructor(private sessionSt: SessionStorageService,
    private router: Router) { }

  ngOnInit() {
    if (this.sessionSt.retrieve('username') === null) {
      this.router.navigateByUrl('/');
    }
  }
}
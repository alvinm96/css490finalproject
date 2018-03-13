import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { SessionStorageService } from 'ngx-webstorage';

@Component({
  selector: 'images',
  templateUrl: './images.component.html'
})

export class ImagesComponent implements OnInit {
  images: any[] = [];

  constructor(private sessionSt: SessionStorageService,
              private router: Router,
              private http: Http) { }

  ngOnInit() {
    if (this.sessionSt.retrieve('username') === null) {
      this.router.navigateByUrl('/');
    }

    let params = new URLSearchParams();
    params.append('userName', this.sessionSt.retrieve('username'));  

    this.http.get('/api/Images', { params: params })
      .toPromise()
      .then((res) => {
        this.images = res.json().results;
      })
      .catch((err) => {
        console.log(err);
      });

  }
}
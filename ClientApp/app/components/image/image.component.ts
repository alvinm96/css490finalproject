import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute, Router } from '@angular/router';
import { SessionStorageService } from 'ngx-webstorage';

@Component({
  selector: 'image',
  templateUrl: './image.component.html'
})

export class ImageComponent implements OnInit {
  info: any;

  constructor(private http: Http,
              private activatedRoute: ActivatedRoute,
              private sessionSt: SessionStorageService,
              private router: Router) { }

  ngOnInit() {
    if (this.sessionSt.retrieve('username') === null) {
      this.router.navigateByUrl('/');
    }

    let params = new URLSearchParams();
    params.append('imageId', this.activatedRoute.pathFromRoot[1].snapshot.url[1].path);

    this.http.get('/api/Images', { params: params })
      .toPromise()
      .then((res) => {
        let result = res.json().results[0];

        if (result) {
          this.info = result;
        } else {
          alert('Not Found');
        }
        
      })
      .catch((err) => {
        console.log(err);
      });
  }
}
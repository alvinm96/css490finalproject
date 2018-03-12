import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SessionStorageService } from 'ngx-webstorage';

@Component({
  selector: 'user',
  templateUrl: './user.component.html'
})

export class UserComponent implements OnInit {
  constructor(private sessionSt: SessionStorageService,
    private router: Router) { }

  ngOnInit() {
    if (this.sessionSt.retrieve('username') === null) {
      this.router.navigateByUrl('/');
    }
  }
}
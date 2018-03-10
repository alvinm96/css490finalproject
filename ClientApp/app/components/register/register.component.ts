import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Http, Headers, Response } from '@angular/http';
import { Router } from '@angular/router';
import { SessionStorageService } from 'ngx-webstorage';
import 'rxjs/add/operator/toPromise';

@Component({
  selector: 'register',
  templateUrl: './register.component.html'
})

export class RegisterComponent {
  constructor(private http: Http,
              private router: Router,
              private sessionSt: SessionStorageService) { }

  register(form: any) {
    var opts = {
      username: form.value.username,
      email: form.value.email,
      password: form.value.password
    };

    this.http.post('/api/Register', opts)
      .toPromise()
      .then((res) => {
        this.router.navigateByUrl('/callback');
      })
      .catch((err) => {
        alert(err.json().response);
      });
  }

  login(form: any) {
    var opts = {
      username: form.value.username,
      password: form.value.password
    };

    this.http.post('/api/Login', opts)
      .toPromise()
      .then((res) => {
        this.sessionSt.store('isLoggedIn', true);
        this.sessionSt.store('username', opts.username);
        this.router.navigateByUrl('/home').then(() => {
          location.reload();
        });
      })
      .catch((err) => {
        alert(err.json().response);
      });
  }
}
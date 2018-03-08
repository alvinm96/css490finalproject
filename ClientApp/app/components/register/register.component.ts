import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Http, Headers, Response } from '@angular/http';
import 'rxjs/add/operator/toPromise';

@Component({
  selector: 'register',
  templateUrl: './register.component.html'
})

export class RegisterComponent {
  constructor(private http: Http) { }

  register(form: any) {
    var opts = {
      username: form.value.username,
      email: form.value.email,
      password: form.value.password
    };

    console.log(form.value.username);

    this.http.post('/api/Register', opts)
      .toPromise()
      .then((res) => {
        // go to callback url
      })
      .catch((err) => {
        alert("Could not register");
      });
  }
}
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Http } from '@angular/http';
import { Router } from '@angular/router';

@Component({
  selector: 'code-verification',
  templateUrl: './code-verification.component.html'
})

export class CodeVerificationComponent {
  constructor(private http: Http,
              private router: Router) { }

  verifyCode(form: NgForm) {
    var opts = {
      username: form.value.username,
      code: form.value.code
    };

    this.http.post('/api/Verify', opts)
      .toPromise()
      .then((res) => {
        this.router.navigateByUrl("/");
      })
      .catch((err) => {
         alert('Error in verifying code')
      });
  }
}
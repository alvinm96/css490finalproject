import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'callback',
  templateUrl: './callback.component.html'
})

export class CallbackComponent implements OnInit {
  codeVerification = 'http://localhost:64838/code-verification';

  constructor(private router: Router) { }

  ngOnInit() { }
}
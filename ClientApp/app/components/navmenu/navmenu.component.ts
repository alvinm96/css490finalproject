import { Component, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { SessionStorageService } from 'ngx-webstorage';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
  isLoggedIn: boolean = false;
  name: string;
  modalRef: BsModalRef;
  userPage: string;

  constructor(private modalService: BsModalService,
              private sessionSt: SessionStorageService) { }

  ngOnInit() {
    this.isLoggedIn = this.sessionSt.retrieve("isLoggedIn");
    this.name = this.sessionSt.retrieve("username");
    this.userPage = '/u/' + this.name;
  }

  openModal(template: any) {
    this.modalRef = this.modalService.show(template);
  }
}

import { Component, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { SessionStorageService } from 'ngx-webstorage';
import { Http } from '@angular/http';

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
  file: any;
  isClicked: boolean = false;

  constructor(private modalService: BsModalService,
              private sessionSt: SessionStorageService,
              private http: Http) { }

  ngOnInit() {
    this.isLoggedIn = this.sessionSt.retrieve("isLoggedIn");
    this.name = this.sessionSt.retrieve("username");
    this.userPage = '/u/' + this.name;
  }

  openModal(template: any) {
    this.modalRef = this.modalService.show(template);
  }

  fileUpload(input: any) {
    this.file = input.target.files;
  }

  upload(form: any) {
    console.log(this.file[0]);

    let body = {
      imageName: form.value.name,
      groupName: 'test-group',
      userName: this.name,
      description: form.value.description,
      imageObj: this.file[0] || ''
    };

    console.log(body);

    this.isClicked = true;

    this.http.post('/api/Images', body)
      .toPromise()
      .then((res: any) => {
        alert('File uploaded.');
        this.modalRef.hide();
        this.isClicked = false;
      })
      .catch((err: any) => {
        console.log(err);
        this.isClicked = false;
      });
  }
}

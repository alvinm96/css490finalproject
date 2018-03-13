import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
  userImages: string;
  userGroups: string;
  file: any;
  isClicked: boolean = false;
  groups: string[] = [];

  constructor(private modalService: BsModalService,
              private sessionSt: SessionStorageService,
              private http: Http,
              private router: Router) { }

  ngOnInit() {
    if (this.name = this.sessionSt.retrieve("username")) {
      this.isLoggedIn = true;
    }
    this.userPage = '/u/' + this.name;
    this.userImages = '/u/' + this.name + '/images';
    this.userGroups = '/u/' + this.name + '/groups';
  }

  openModal(template: any) {
    this.modalRef = this.modalService.show(template);
  }

  fileUpload(input: any) {
    this.file = input.target.files;
  }

  signOut() {
    let body = {
      userName: this.name
    };

    this.http.post('/api/Logout', body)
      .toPromise()
      .then((res) => {
        this.isLoggedIn = false;
        this.sessionSt.clear('username');
        this.router.navigateByUrl('/');
        
      })
      .catch((err) => {
        console.log(err);
      });
  }

  upload(form: any) {
    let reader = new FileReader();

    reader.readAsArrayBuffer(this.file[0]);

    reader.onload = () => {
      var byteArray = new Uint8Array(reader.result);

      if (byteArray.length > 65000) {
        return alert('File size too big! Has to be less than 65KB');
      }

      let body = {
        imageName: form.value.name,
        groupName: 'test-group',
        userName: this.name,
        description: form.value.description,
        imageObj: btoa(String.fromCharCode.apply(null, byteArray))
      };

      this.isClicked = true;

      this.http.post('/api/Image', body)
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
}

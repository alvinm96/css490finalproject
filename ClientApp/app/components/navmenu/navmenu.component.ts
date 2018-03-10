import { Component } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
  modalRef: BsModalRef;
  constructor(private modalService: BsModalService) { }

  openModal(template: any) {
    this.modalRef = this.modalService.show(template);
  }
}

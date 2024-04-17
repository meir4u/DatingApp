import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-roles-modal',
  templateUrl: './roles-modal.component.html',
  styleUrls: ['./roles-modal.component.scss']
})
export class RolesModalComponent implements OnInit{
  title: string = 'Edit Roles for';
  closeBtnName: string = 'Submit';
  username = '';
  availableRoles: any[] = [];
  selectedRoles: any[] = [];

  constructor(public bsModalRef: BsModalRef) { }
  ngOnInit(): void {

  }

  updateChecked(checkedValue: string) {
    const index = this.selectedRoles.indexOf(checkedValue);

    if (index !== -1) {
      this.selectedRoles.splice(index, 1)
    } else {
      this.selectedRoles.push(checkedValue);
    }
  }
}

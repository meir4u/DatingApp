import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../_services/admin.service';
import { User } from '../../_models/user';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { RolesModalComponent } from '../../modals/roles-modal/roles-modal.component';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.scss']
})
export class UserManagementComponent implements OnInit {
  users: User[] = [];
  bsModalRef: BsModalRef<RolesModalComponent> = new BsModalRef<RolesModalComponent>();

  constructor(
    private adminService: AdminService,
    private modalService: BsModalService,
  ) { }

    ngOnInit(): void {
      this.getUsersWithRoles();
    }

  getUsersWithRoles() {
    this.adminService.getUsersWithRoles().subscribe({
      next: users => {
        if (users) {
          this.users = users
        }
      },
      error: error => console.log(error)
    });
  }

  openRolesModal() {
    const initialState: ModalOptions = {
      initialState: {
        list: [
          'do thing',
          'another thing',
          'something else',
        ],
        title: 'Test Modal'
      }
    }
    this.bsModalRef = this.modalService.show(RolesModalComponent, initialState);
    this.bsModalRef.content!.closeBtnName = 'Close';
  }

}

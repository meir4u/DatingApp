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
  availableRoles = [
    'Admin',
    'Moderator',
    'Member',
  ];

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

  openRolesModal(user: User) {
    const config = {
      class: 'modal-dialog-centered',
      initialState: {
        username: user.username,
        availableRoles: this.availableRoles,
        selectedRoles: [...user.roles] 
      }
    }

    console.log(user.roles);
    console.log(config);

    this.bsModalRef = this.modalService.show(RolesModalComponent, config);
    this.bsModalRef.onHidden?.subscribe({
      next: () => {
        const selectedRoles = this.bsModalRef.content?.selectedRoles
        const isEqual = this.arrayEqual(selectedRoles, user.roles);

        if (false == isEqual && selectedRoles) {
          this.adminService.updateUserRoles(user.username, selectedRoles.join(',')).subscribe({
            next: roles => user.roles = roles,
          })
        }
      },
    })
  }

  private arrayEqual(arr1: any, arr2: any) {
    return JSON.stringify(arr1.sort()) === JSON.stringify(arr2.sort());
  }

}

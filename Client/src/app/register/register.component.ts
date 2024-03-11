import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  
  @Input() usersFromHomeComponent: any;
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(
    private accountService: AccountService,
    private toastr: ToastrService
  ) { }

  register() {
    this.accountService.register(this.model).subscribe({
      next: () => this.cancel(),
      error: error => {
        this.toastr.error(error.error);
        console.log(error);
      },
      complete: () => console.log('completed!')
    });
    console.log(this.model);
  }

  cancel() {

    this.cancelRegister.emit(false);
  }
}

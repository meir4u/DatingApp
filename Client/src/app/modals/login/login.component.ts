import { Component, NgZone } from '@angular/core';
import { AccountService } from '../../_services/account.service';
import { Router } from '@angular/router';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  model: any = {};

  title: string = 'Login';
  closeBtnName: string = 'Submit';
  isLoginFailed = false;
  isGoogleLoginFailed = false;
  constructor(public accountService: AccountService,
    private router: Router,
    public bsModalRef: BsModalRef,
    private ngZone: NgZone) {

  }
  login() {
    this.accountService.login(this.model).subscribe({
      next: _ => {
        this.ngZone.run(() => {
          this.model = {};
          this.bsModalRef.hide();
          this.router.navigateByUrl('/members');
        });
        
      },
      error: error => {
        this.isLoginFailed = true;
        this.isGoogleLoginFailed = false;
        console.error('Login failed', error);
      }
    })
    console.log(this.model);
  }

  loginWithGoogle() {
    this.accountService.loginGoogle().subscribe(
      response => {
        this.ngZone.run(() => {
          this.model = {};
          this.bsModalRef.hide();
          this.router.navigateByUrl('/members');
        });
      },
      error => {
        this.isLoginFailed = false;
        this.isGoogleLoginFailed = true;
        console.error('Google sign-in failed', error);
      }
    );
    console.log(this.model);
  }
}

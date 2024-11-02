import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of, tap } from 'rxjs';
import { User } from '../_models/user';
import { Route, Router } from '@angular/router';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { LoginComponent } from '../modals/login/login.component';
import { environment } from '../../environments/environment';
import { ArialCurrentDirective } from '../_directives/arial-current.directive';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  bsModalRef: BsModalRef<LoginComponent> = new BsModalRef<LoginComponent>();
  model: any = {};
  isDevelopment = environment.production == false;
  appName = environment.applicationName;

  constructor(
              public accountService: AccountService,
    private router: Router,
    private toastr: ToastrService,
    private modalService: BsModalService
  ) { }

  ngOnInit(): void {
    }
    

  openLoginModal() {

    this.bsModalRef = this.modalService.show(LoginComponent);
    this.bsModalRef.onHidden?.pipe(
      tap(() => {
        this.modalService.hide();
      }),
      tap(() => {
        this.router.navigateByUrl('/');
      }),
    ).subscribe();
  }

  login() {
    this.accountService.login(this.model).subscribe({
      next: _ => {
        this.router.navigateByUrl('/');
        this.model = {};
      },
    })
    console.log(this.model);
  }

  loginWithGoogle() {
    this.accountService.loginGoogle().subscribe(
      response => {
        console.log('Google sign-in successful', response);
        this.router.navigateByUrl('/');
        this.model = {};
      },
      error => {
        console.error('Google sign-in failed', error);
      }
    );
    console.log(this.model);
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

}

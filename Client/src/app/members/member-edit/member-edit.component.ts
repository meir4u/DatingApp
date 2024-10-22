import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { Member } from '../../_models/member';
import { User } from '../../_models/user';
import { AccountService } from '../../_services/account.service';
import { MembersService } from '../../_services/members.service';
import { BehaviorSubject, take } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { AbstractControl, FormBuilder, FormGroup, NgForm, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MemberMessagesComponent } from '../member-messages/member-messages.component';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.scss']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm | undefined;

  editMemberForm: FormGroup = new FormGroup({});

  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm?.dirty) {
      $event.returnValue = true;
    }
  }
  member: Member | undefined;
  member$: BehaviorSubject<Member | null> = new BehaviorSubject<Member | null>(null);
  user: User | null = null;

  constructor(
    private accountService: AccountService,
    private memberService: MembersService,
    private fb: FormBuilder,
    private toastr: ToastrService,
  ) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => this.user = user
    })
  }
  ngOnInit(): void {
      this.initializeForm();
      this.loadMember();
      this.patchFormValues();

    }

  initializeForm() {
      this.editMemberForm = this.fb.group({
        introduction: ['', [Validators.required, this.nonWhitespaceValidator()]],
        lookingFor: ['', [Validators.required, this.nonWhitespaceValidator()]],
        interests: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        city: ['', [Validators.required, this.nonWhitespaceValidator()]],
        country: ['', [Validators.required, this.nonWhitespaceValidator()]]
      });
    
  }

  patchFormValues() {
    this.member$.subscribe({
      next: member => {
        if (member) {
          this.editMemberForm.controls['introduction'].patchValue(member.introduction);
          this.editMemberForm.controls['lookingFor'].patchValue(member.lookingFor);
          this.editMemberForm.controls['interests'].patchValue(member.interests);
          this.editMemberForm.controls['email'].patchValue(member.email);
          this.editMemberForm.controls['city'].patchValue(member.city);
          this.editMemberForm.controls['country'].patchValue(member.country);
        }
      }
    })
  }

  nonWhitespaceValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const isWhitespace = (control.value || '').trim().length === 0;
      const isValid = !isWhitespace;
      return isValid ? null : { 'whitespace': true };
    };
  }

  loadMember() {
    if (!this.user) return;

    this.memberService.getMember(this.user.username).subscribe({
      next: member => {
        this.member = member;
        this.member$.next(member);
      },
      error: error => console.log(error),
    })
  }


  updateMember() {
    this.memberService.updateMember(this.editForm?.value).subscribe({
      next: _ => {
        this.toastr.success('Profile updated successully');
        this.editForm?.reset(this.member);
      },
      error: error => console.log(error),
    });
    

  }

}

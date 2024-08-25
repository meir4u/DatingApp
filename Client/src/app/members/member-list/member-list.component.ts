import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Member } from '../../_models/member';
import { MembersService } from '../../_services/members.service';
import { Observable, take } from 'rxjs';
import { Pagination } from '../../_models/pagination';
import { UserParams } from '../../_models/userParams';
import { AccountService } from '../../_services/account.service';
import { User } from '../../_models/user';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.scss']
})
export class MemberListComponent implements OnInit {
  //members$: Observable<Member[]> | undefined;
  members: Member[] = [];
  pagination: Pagination | undefined;
  userParams: UserParams | undefined;
  genderList = [{ value: 'male', display: 'Males' }, { value: 'female', display: 'Females' }];

  constructor(
    private memberService: MembersService,
  )
  {
    this.userParams = this.memberService.getUserParams();
  }


  ngOnInit(): void {
    //this.members$ = this.memberService.getMembers();
    this.loadMemmbers();
    }

  loadMemmbers() {
    if (this.userParams) {
      this.memberService.setUserParams(this.userParams);

      this.memberService.getMembers(this.userParams).subscribe({
        next: response => {
          if (response.result && response.pagination) {
            debugger;
            this.members = response.result;
            this.pagination = response.pagination;
          }
        }
      });
    }

  }

  resetFilters() {
    this.userParams = this.memberService.resetUserParams();
    this.loadMemmbers();
  }

  pageChanged(event: any) {
    if (this.userParams && this.userParams?.pageNumber !== event.page) {
      this.userParams.pageNumber = event.page;
      this.memberService.setUserParams(this.userParams);
      this.loadMemmbers();
      //event.stopPropagation();
      //this.changeDetectorRef.(); // Manually trigger change detection
    }
  }
}

import { Component, Input, ViewEncapsulation } from '@angular/core';
import { Member } from '../../_models/member';
import { MembersService } from '../../_services/members.service';
import { ToastrService } from 'ngx-toastr';
import { PresenceService } from '../../_services/presence.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.scss',]
})
export class MemberCardComponent {
  @Input() member: Member | undefined;

  constructor(private memberService: MembersService, private toastr: ToastrService, public presenceSerivce: PresenceService) { }

  addLike(member: Member) {
    this.memberService.addLike(member.userName).subscribe({
      next: () => this.toastr.success('You have liked ' + member.knownAs),
      error: (error) => console.log(error),
    });
  }

  removeLike(member: Member) {
    this.memberService.removeLike(member.userName).subscribe({
      next: () => this.toastr.success('You have remove like for ' + member.knownAs),
      error: (error) => console.log(error),
    });
  }
}

import { Component, OnInit } from '@angular/core';
import { Member } from '../_models/member';
import { MembersService } from '../_services/members.service';
import { Pagination } from '../_models/pagination';
import { ELikePredicate } from '../_models/enums/enum-like-predicate';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.scss']
})
export class ListsComponent implements OnInit {
  members: Member[] | undefined;
  ELikePredicate = ELikePredicate;
  predicate = ELikePredicate.LikedByUser;
  pageNumber = 1;
  pageSize = 5;
  pagination: Pagination | undefined;

  constructor(private memberService: MembersService) { }

    ngOnInit(): void {
      this.loadLikes();
    }

  loadLikes() {
    this.memberService.getLikes(this.predicate, this.pageNumber, this.pageSize).subscribe({
      next: response => {
        this.members = response.result;
        this.pagination = response.pagination;
      },
      error: error => console.log(error)
    })
  }

  pageChanged(event: any) {
    if (this.pageNumber !== event.page) {
      this.pageNumber = event.page;
      this.loadLikes();
    }
  }
}

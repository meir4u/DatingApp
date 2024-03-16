import { Component, OnInit } from '@angular/core';
import { Member } from '../../_models/member';
import { MembersService } from '../../_services/members.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';

@Component({
  selector: 'app-member-details',
  standalone: true,
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.scss'],
  imports: [
    CommonModule,
    TabsModule,
    GalleryModule,
  ]
})
export class MemberDetailsComponent implements OnInit {
  member: Member | undefined;
  images: GalleryItem[] = [];
  constructor(
    private memberService: MembersService,
    private route: ActivatedRoute,
  ) { }

    ngOnInit(): void {
      this.loadMember() 
    }

  loadMember() {
    var username = this.route.snapshot.paramMap.get('username');
    if (!username) return;
    this.memberService.getMember(username).subscribe({
      next: member => {
        this.member = member;
        this.getImages();
        },
      error: error=>console.log(error),
    });
  }

  getImages() {
    if (!this.member) return;

    for (const photo of this.member?.photos) {
      this.images.push(
        new ImageItem({
          src: photo.url,
          thumb: photo.url,
        }),
      )
    }
  }
}

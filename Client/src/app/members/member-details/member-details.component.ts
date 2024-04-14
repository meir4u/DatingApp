import { Component, OnInit, ViewChild } from '@angular/core';
import { Member } from '../../_models/member';
import { MembersService } from '../../_services/members.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TabDirective, TabsModule, TabsetComponent } from 'ngx-bootstrap/tabs';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';
import { TimeagoModule } from 'ngx-timeago';
import { MemberMessagesComponent } from '../member-messages/member-messages.component';
import { MessageService } from '../../_services/message.service';
import { Message } from '../../_modules/massage';

@Component({
  selector: 'app-member-details',
  standalone: true,
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.scss'],
  imports: [
    CommonModule,
    TabsModule,
    GalleryModule,
    TimeagoModule,
    MemberMessagesComponent,
  ]
})
export class MemberDetailsComponent implements OnInit {
  @ViewChild('memberTabs', { static: true }) memberTabs?: TabsetComponent;
  member: Member = {} as Member;
  images: GalleryItem[] = [];
  activeTab?: TabDirective;
  messages: Message[] = [];

  constructor(
    private memberService: MembersService,
    private route: ActivatedRoute,
    private messageServie: MessageService,
  ) { }

  ngOnInit(): void {
    this.route.data.subscribe({
      next: data => this.member = data['member'],
      error: error => console.log(error),
    });
    this.route.queryParams.subscribe({
      next: params => {
        params['tab'] && this.selectTab(params['tab']);
      }
    });
    this.getImages();
    }

  onTabActivated(data: TabDirective) {
    this.activeTab = data;
    if (this.activeTab?.heading === 'Messages') {
      this.loadMessages();
    }
  }

  loadMessages() {
    if (this.member) {
      this.messageServie.getMessageThread(this.member.userName).subscribe({
        next: messages => this.messages = messages,
        error: error => console.log(error),
      })
    }
  }

  selectTab(heading: string) {
    if (this.memberTabs) {
      this.memberTabs.tabs.find(x => x.heading === heading)!.active = true;
    }
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

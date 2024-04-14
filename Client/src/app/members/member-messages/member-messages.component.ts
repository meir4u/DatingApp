import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Message } from '../../_modules/massage';
import { MessageService } from '../../_services/message.service';
import { CommonModule } from '@angular/common';
import { TimeagoModule } from 'ngx-timeago';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-messages',
  standalone: true,
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.scss'],
  imports: [CommonModule, TimeagoModule, FormsModule]
})
export class MemberMessagesComponent implements OnInit {
  @ViewChild('messageForm') messageForm: NgForm | undefined;
  @Input() messages: Message[] = [];
  @Input() username: string | undefined;
  messageContent = '';
  constructor(
    private messageService: MessageService,
  ) { }

    ngOnInit(): void {
    }

  sendMessage() {
    if (!this.username) return;
    this.messageService.sendMessage(this.username, this.messageContent).subscribe({
      next: message => {
        this.messages.push(message);
        this.messageForm?.reset();
      },
      error: error=>console.log(error),
    })
  }

}

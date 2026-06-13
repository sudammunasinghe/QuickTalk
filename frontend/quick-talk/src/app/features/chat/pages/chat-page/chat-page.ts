import { Component } from '@angular/core';
import { ChatItem } from '../../components/chat-item/chat-item';

@Component({
  selector: 'app-chat-page',
  imports: [
    ChatItem
  ],
  templateUrl: './chat-page.html',
  styleUrl: './chat-page.scss',
})
export class ChatPage {}

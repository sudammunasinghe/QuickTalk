import { Component } from '@angular/core';
import { ChatItem } from '../../components/chat-item/chat-item';
import { ChatService } from '../../../../core/services/chat/chat-service';
import { ChatItemResponse } from '../../../../core/models/chat/chat-item-response';
import { CommonModule } from '@angular/common';
import { SidebarHeader } from '../../components/sidebar-header/sidebar-header';
import { ChatHeader } from '../../components/chat-header/chat-header';

@Component({
    selector: 'app-chat-page',
    imports: [
        ChatItem,
        CommonModule,
        SidebarHeader,
        ChatHeader
    ],
    templateUrl: './chat-page.html',
    styleUrl: './chat-page.scss',
})
export class ChatPage {
    chatItems: ChatItemResponse[] = [];
    firstName = 'Sudam';
    lastName = 'Munasinghe';
    constructor(
        private chatService: ChatService
    ){}

    ngOnInit(){
        this.chatService.GetConversationsAsync()
            .subscribe({
                next: (response) => {
                    this.chatItems = response.data;
                }
            });
    }
}

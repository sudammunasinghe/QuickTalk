import { Component, EventEmitter, Input, Output, output } from '@angular/core';
import { ChatItemResponse } from '../../../../core/models/chat/chat-item-response';
import { CommonModule } from '@angular/common';
import { ChatService } from '../../../../core/services/chat/chat-service';

@Component({
    selector: 'app-chat-item',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './chat-item.html',
    styleUrl: './chat-item.scss',
})
export class ChatItem {
    @Input() chatItem!: ChatItemResponse;
    constructor(
        private chatService: ChatService
    ) { }

    getInitials(): string {
        return (
            (this.chatItem?.firstName?.charAt(0) ?? '') +
            (this.chatItem?.lastName?.charAt(0) ?? '')
        ).toUpperCase();
    }

    selectedChat() {
        this.chatService.setselectedChat(this.chatItem);
    }
}

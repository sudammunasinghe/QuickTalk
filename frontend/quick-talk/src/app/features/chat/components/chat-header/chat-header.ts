import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { ChatItemResponse } from '../../../../core/models/chat/chat-item-response';
import { ChatService } from '../../../../core/services/chat/chat-service';

@Component({
    selector: 'app-chat-header',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './chat-header.html',
    styleUrl: './chat-header.scss',
})
export class ChatHeader {
    selectedChat!: ChatItemResponse | null;
    constructor(
        private chatService: ChatService
    ) { }

    getInitials() {
        return (
            (this.selectedChat?.firstName?.charAt(0) ?? '') + (this.selectedChat?.lastName?.charAt(0) ?? '')
        ).toUpperCase();
    }

    ngOnInit() {
        this.chatService.selectedChat$
            .subscribe(chat => {
                this.selectedChat = chat;
            })
    }
}

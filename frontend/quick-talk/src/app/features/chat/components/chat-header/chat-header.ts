import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { ChatItemResponse } from '../../../../core/models/chat/chat-item-response';
import { ChatService } from '../../../../core/services/chat/chat-service';
import { Signalr } from '../../../../core/services/signalr/signalr';

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
        private chatService: ChatService,
        private signalRChatService: Signalr
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

    isOnline(userId: number): boolean {
        return this.signalRChatService.onlineUsers.has(userId.toString());
    }
}

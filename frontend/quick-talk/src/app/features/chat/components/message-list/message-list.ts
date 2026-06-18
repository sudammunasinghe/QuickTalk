import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ChatService } from '../../../../core/services/chat/chat-service';
import { CoversationHistory } from '../../../../core/models/chat/coversation-history';
import { CommonModule } from '@angular/common';
import { ChatItemResponse } from '../../../../core/models/chat/chat-item-response';

@Component({
    selector: 'app-message-list',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './message-list.html',
    styleUrl: './message-list.scss',
})
export class MessageList {
    messages: CoversationHistory[] = [];

    constructor(
        private chatService: ChatService
    ) { }

    ngOnInit() {
        this.chatService.selectedChat$
            .subscribe(chat => {
                if (!chat) return;
                console.log('LOAD CHAT:', chat.userId);
                this.loadMessages(chat.userId);
            });
    }

    loadMessages(receiverId: number) {
        console.log('old messages', this.messages);
        this.chatService.GetConversationHistoryAsync(receiverId)
            .subscribe({
                next: (response) => {
                    if (response.data && response.isSuccess) {
                        console.log('API:', response.data);
                        this.messages = [...response.data];
                        console.log('new messages',this.messages);
                    } else {
                        this.messages = [];
                    }
                }
            });
    }
}

import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ChatService } from '../../../../core/services/chat/chat-service';
import { CoversationHistory } from '../../../../core/models/chat/coversation-history';
import { CommonModule } from '@angular/common';
import { ChatItemResponse } from '../../../../core/models/chat/chat-item-response';
import { Signalr } from '../../../../core/services/signalr/signalr';
import { Subject, takeUntil } from 'rxjs';

@Component({
    selector: 'app-message-list',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './message-list.html',
    styleUrl: './message-list.scss',
})
export class MessageList {
    messages: CoversationHistory[] = [];
    currentChatUserId: number | null = null;
    private destroy$ = new Subject<void>();
    constructor(
        private chatService: ChatService,
        private signalRChatService: Signalr
    ) { }

    ngOnInit() {
        // this.chatService.selectedChat$
        //     .subscribe(chat => {
        //         if (!chat) return;
        //         console.log('LOAD CHAT:', chat.userId);
        //         this.loadMessages(chat.userId);
        //     });

        this.chatService.selectedChat$
            .pipe(takeUntil(this.destroy$))
            .subscribe(chat => {
                if (!chat) return;
                console.log('CHAT SELECTED:', chat.userId);
                this.currentChatUserId = chat.userId;
                this.loadMessages(chat.userId);
            });

        // Listen for real time  messages
        this.signalRChatService.message$
            .pipe(takeUntil(this.destroy$))
            .subscribe(msg => {
                if (!this.currentChatUserId) return;

                // only show message if it's from current chat
                if (msg.senderId != this.currentChatUserId) return;

                console.log('REALTIME MESSAGE:', msg);
                const realtimeMessage: CoversationHistory = {
                    id: 0,
                    senderId: Number(msg.senderId),
                    message: msg.message,
                    sendAt: new Date(),
                    isMine: false
                };

                this.messages = [
                    ...this.messages,
                    realtimeMessage
                ];
                console.log('hoooo',this.messages);
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
                        console.log('new messages', this.messages);
                    } else {
                        this.messages = [];
                    }
                }
            });
    }

    ngOnDestroy() {
        this.destroy$.next();
        this.destroy$.complete();
    }
}

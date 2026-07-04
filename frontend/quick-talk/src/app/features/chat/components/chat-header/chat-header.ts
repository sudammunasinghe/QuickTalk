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

    getStatusText(userId: number | undefined): string {
        if (!userId) return 'Offline';
        const status = this.signalRChatService.getStatus(userId.toString());
        if (status == 'Online') return 'Online';
        if (status == 'Away') return 'Away';
        return 'Offline';
    }

    getLastSeenText(userId: number | undefined): string {
        if (!userId) return '';

        const lastSeen = this.signalRChatService.getLastSeen(userId.toString());
        if (lastSeen) {
            const date = new Date(lastSeen);
            const day = new Date(lastSeen).getDate();
            const time = new Date(lastSeen).toLocaleTimeString([], {
                hour: '2-digit',
                minute: '2-digit',
                hour12: false
            });

            if (day == new Date().getDate())
                return `Last seen today at ${time}`;
            else if (day == new Date().getDate() - 1)
                return `Last seen yesterday at ${time}`;
            else {
                const formatted =
                    `${date.getFullYear()}/` +
                    `${String(date.getMonth() + 1).padStart(2, '0')}/` +
                    `${String(date.getDate()).padStart(2, '0')}`

                return `Last seen ${formatted} at ${time}`;
            }
        }
        return 'Offline';
    }
}

import { Component, EventEmitter, Input, Output, output } from '@angular/core';
import { ChatItemResponse } from '../../../../core/models/chat/chat-item-response';
import { CommonModule } from '@angular/common';
import { ChatService } from '../../../../core/services/chat/chat-service';
import { Signalr } from '../../../../core/services/signalr/signalr';
import { UserService } from '../../../../core/services/user/user-service';
import { DialogModule } from 'primeng/dialog';
import { ChatItemProfileCard } from './chat-item-profile-card/chat-item-profile-card';

@Component({
    selector: 'app-chat-item',
    standalone: true,
    imports: [
        CommonModule,
        DialogModule,
        ChatItemProfileCard
    ],
    templateUrl: './chat-item.html',
    styleUrl: './chat-item.scss',
})
export class ChatItem {
    @Input() chatItem!: ChatItemResponse;
    displayChatItemProfileCard = false;
    constructor(
        private chatService: ChatService,
        private signalRChatService: Signalr,
        private userService: UserService
    ) { }

    getInitials(): string {
        return (
            (this.chatItem?.firstName?.charAt(0) ?? '') +
            (this.chatItem?.lastName?.charAt(0) ?? '')
        ).toUpperCase();
    }

    selectedChat() {
        this.chatService.setselectedChat(this.chatItem);
        this.userService.getLastSeen(this.chatItem.userId)
            .subscribe(lastSeen => {
                this.signalRChatService.setLastSeen(
                    this.chatItem.userId.toString(),
                    lastSeen
                );
            });
    }

    getStatusText(userId: number) {
        const status = this.signalRChatService.getStatus(userId.toString());
        if (status == 'Online') return 'Online';
        if (status == 'Away') return 'Away';
        return 'Offline';
    }

    showChatItemProfileCard(): void {
        this.displayChatItemProfileCard = true;
    }
}

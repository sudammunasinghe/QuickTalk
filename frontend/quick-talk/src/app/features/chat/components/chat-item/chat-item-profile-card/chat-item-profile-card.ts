import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ChatItemResponse } from '../../../../../core/models/chat/chat-item-response';
import { CommonModule, DatePipe } from '@angular/common';
import { Signalr } from '../../../../../core/services/signalr/signalr';
import { ChatService } from '../../../../../core/services/chat/chat-service';
import { UserService } from '../../../../../core/services/user/user-service';

@Component({
    selector: 'app-chat-item-profile-card',
    standalone: true,
    imports: [
        CommonModule,
        DatePipe
    ],
    templateUrl: './chat-item-profile-card.html',
    styleUrl: './chat-item-profile-card.scss',
})
export class ChatItemProfileCard {
    @Input() chatItemDetails!: ChatItemResponse;
    @Output() close = new EventEmitter<void>();

    constructor(
        private signalRChatService: Signalr,
        private chatService: ChatService,
        private userService: UserService
    ) { }

    closeProfileCard(): void {
        this.close.emit();
    }

    getInitials(): string {
        return (
            (this.chatItemDetails?.firstName.charAt(0) ?? '') +
            (this.chatItemDetails?.lastName.charAt(0) ?? '')
        ).toUpperCase();
    }

    getStatus(userId: number): string {
        const status = this.signalRChatService.getStatus(userId.toString());
        if (status == 'Online') return 'Online';
        if (status == 'Away') return 'Away';
        return 'Offline';
    }

    getStatusColor(userId: number): string {
        const status = this.getStatus(userId);
        switch (status) {
            case 'Online':
                return '#22c55e';
            case 'Away':
                return '#f59e0b';
            case 'Offline':
                return '#64748b';
            default:
                return '#64748b'
        }
    }

    selectedChat() {
        this.chatService.setselectedChat(this.chatItemDetails);
        this.userService.getLastSeen(this.chatItemDetails.userId)
            .subscribe(lastSeen => {
                this.signalRChatService.setLastSeen(
                    this.chatItemDetails.userId.toString(),
                    lastSeen
                );
            });
        this.closeProfileCard();
    }
}

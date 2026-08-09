import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Signalr } from '../../../../../../core/services/signalr/signalr';
import { CommonModule } from '@angular/common';
import { ChatService } from '../../../../../../core/services/chat/chat-service';
import { UserService } from '../../../../../../core/services/user/user-service';
import { ChatItemResponse } from '../../../../../../core/models/chat/chat-item-response';

@Component({
    selector: 'app-user-item',
    imports: [
        CommonModule
    ],
    templateUrl: './user-item.html',
    styleUrl: './user-item.scss',
})
export class UserItem {
    @Input() chatItem!: ChatItemResponse;
    @Output() newChatSelected = new EventEmitter<boolean>();
    constructor(
        private realTimeService: Signalr,
        private chatService: ChatService,
        private userService: UserService
    ) { }

    getInitials(): string {
        return (
            (this.chatItem?.firstName?.charAt(0) ?? '') +
            (this.chatItem?.lastName.charAt(0) ?? '')
        ).toUpperCase();
    }

    getStatusColor(userId: number): string {
        const status = this.realTimeService.getStatus(userId.toString());
        if (status == 'Online') return '#22c55e';
        if (status == 'Away') return '#f59e0b';
        return '#64748b';
    }

    getStatusText(userId: number): string {
        const status = this.realTimeService.getStatus(userId.toString());
        return status;
    }

    selectedChat() {
        this.chatService.setselectedChat(this.chatItem);
        this.userService.getLastSeen(this.chatItem.userId)
            .subscribe(lastSeen => {
                this.realTimeService.setLastSeen(
                    this.chatItem.userId.toString(),
                    lastSeen
                );
            });
        this.newChatSelected.emit(true);
    }
}

import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ChatItemResponse } from '../../../../../core/models/chat/chat-item-response';
import { CommonModule, DatePipe } from '@angular/common';

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

    closeProfileCard(): void {
        this.close.emit();
    }

    getInitials(): string {
        return (
            (this.chatItemDetails?.firstName.charAt(0) ?? '') +
            (this.chatItemDetails?.lastName.charAt(0) ?? '')
        ).toUpperCase();
    }
}

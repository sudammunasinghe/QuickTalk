import { Component, EventEmitter, Output } from '@angular/core';

@Component({
    selector: 'app-chat-item-profile-card',
    standalone: true,
    imports: [],
    templateUrl: './chat-item-profile-card.html',
    styleUrl: './chat-item-profile-card.scss',
})
export class ChatItemProfileCard {
    @Output() close = new EventEmitter<void>();

    closeProfileCard(): void{
        this.close.emit();
    }
}

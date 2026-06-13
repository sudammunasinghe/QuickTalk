import { Component, Input } from '@angular/core';
import { ChatItemResponse } from '../../../../core/models/chat/chat-item-response';

@Component({
    selector: 'app-chat-item',
    imports: [],
    templateUrl: './chat-item.html',
    styleUrl: './chat-item.scss',
})
export class ChatItem {
    @Input() chatItem!: ChatItemResponse;
}

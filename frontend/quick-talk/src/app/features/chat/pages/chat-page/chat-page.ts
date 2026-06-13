import { Component } from '@angular/core';
import { ChatItem } from '../../components/chat-item/chat-item';
import { ChatService } from '../../../../core/services/chat/chat-service';

@Component({
    selector: 'app-chat-page',
    imports: [
        ChatItem
    ],
    templateUrl: './chat-page.html',
    styleUrl: './chat-page.scss',
})
export class ChatPage {
    constructor(
        private chatService: ChatService
    ){}

    ngOnInit(){
        this.chatService.GetConversationsAsync()
            .subscribe({
                next: (response) => {
                    console.log(response);
                }
            });
    }
}

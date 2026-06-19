import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { ChatService } from '../../../../core/services/chat/chat-service';
import { SendMessage } from '../../../../core/models/chat/send-message';

@Component({
    selector: 'app-message-input',
    imports: [
        FormsModule,
        ButtonModule,
        InputTextModule
    ],
    templateUrl: './message-input.html',
    styleUrl: './message-input.scss',
})
export class MessageInput {
    message = '';
    receiverId!: number;

    constructor(
        private chatService: ChatService
    ){}

    ngOnInit(){
        this.chatService.selectedChat$
            .subscribe(chat =>{
                if(!chat) return;
                this.receiverId = chat.userId;
            });
    }

    sendMessage(): void{
        const request: SendMessage = {
            receiverId: this.receiverId,
            message: this.message
        }

        this.chatService.SendMessageAsync(request)
            .subscribe({
                next: (response) => {
                    
                }
            });
    }
}

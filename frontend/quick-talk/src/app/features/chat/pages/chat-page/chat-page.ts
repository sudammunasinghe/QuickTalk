import { Component } from '@angular/core';
import { ChatItem } from '../../components/chat-item/chat-item';
import { ChatService } from '../../../../core/services/chat/chat-service';
import { ChatItemResponse } from '../../../../core/models/chat/chat-item-response';
import { CommonModule } from '@angular/common';
import { SidebarHeader } from '../../components/sidebar-header/sidebar-header';
import { ChatHeader } from '../../components/chat-header/chat-header';
import { MessageList } from '../../components/message-list/message-list';
import { CoversationHistory } from '../../../../core/models/chat/coversation-history';

@Component({
    selector: 'app-chat-page',
    imports: [
        ChatItem,
        CommonModule,
        SidebarHeader,
        ChatHeader,
        MessageList
    ],
    templateUrl: './chat-page.html',
    styleUrl: './chat-page.scss',
})
export class ChatPage {
    chatItems: ChatItemResponse[] = [];
    selectedChatData: ChatItemResponse | null = null;
    constructor(
        private chatService: ChatService
    ) { }

    ngOnInit() {
        this.loadConversationsAsync();
    }

    loadConversationsAsync() {
        this.chatService.GetConversationsAsync()
            .subscribe({
                next: (response) => {
                    if (response.data && response.isSuccess) {
                        this.chatItems = response.data;

                        //Select first chat automatically
                        if (this.chatItems.length > 0) {
                            this.chatService.setselectedChat(this.chatItems[0]);
                        }
                    }
                }
            });
    }
}

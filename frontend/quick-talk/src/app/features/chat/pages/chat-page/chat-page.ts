import { Component } from '@angular/core';
import { ChatItem } from '../../components/chat-item/chat-item';
import { ChatService } from '../../../../core/services/chat/chat-service';
import { ChatItemResponse } from '../../../../core/models/chat/chat-item-response';
import { CommonModule } from '@angular/common';
import { SidebarHeader } from '../../components/sidebar-header/sidebar-header';
import { ChatHeader } from '../../components/chat-header/chat-header';
import { MessageList } from '../../components/message-list/message-list';
import { CoversationHistory } from '../../../../core/models/chat/coversation-history';
import { MessageInput } from '../../components/message-input/message-input';

@Component({
    selector: 'app-chat-page',
    imports: [
        ChatItem,
        CommonModule,
        SidebarHeader,
        ChatHeader,
        MessageList,
        MessageInput
    ],
    templateUrl: './chat-page.html',
    styleUrl: './chat-page.scss',
})
export class ChatPage {
    chatItems: ChatItemResponse[] = [];
    filteredChatItems: ChatItemResponse[] = [];
    selectedChatData: ChatItemResponse | null = null;
    constructor(
        private chatService: ChatService
    ) { }

    ngOnInit() {
        this.loadConversationsAsync();
    }

    filteredChats(searchText: string): void {
        if (!searchText.trim()) {
            this.filteredChatItems = [...this.chatItems];
            return;
        }

        const search = searchText.toLowerCase();
        this.filteredChatItems = this.chatItems.filter(chat =>
            (`${chat.firstName} ${chat.lastName}`)
                .toLowerCase()
                .includes(search)
        );
    }

    loadConversationsAsync(): void {
        this.chatService.GetConversationsAsync()
            .subscribe({
                next: (response) => {
                    if (response.data && response.isSuccess) {
                        this.chatItems = response.data;
                        this.filteredChatItems = this.chatItems;
                        //Select first chat automatically
                        if (this.chatItems.length > 0) {
                            this.chatService.setselectedChat(this.chatItems[0]);
                        }
                    }
                }
            });
    }
}

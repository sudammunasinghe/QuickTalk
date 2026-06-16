import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { BehaviorSubject, Observable } from 'rxjs';
import { ApiResponse } from '../../models/apiResponse/api-response';
import { ChatItemResponse } from '../../models/chat/chat-item-response';

@Injectable({
    providedIn: 'root',
})
export class ChatService {
    private http = inject(HttpClient);
    private apiUrl = `${environment.apiUrl}/conversation`;

    private selectedChatSource =
        new BehaviorSubject<ChatItemResponse | null>(null);

    selectedChat$ =
        this.selectedChatSource.asObservable();

    setselectedChat(chat: ChatItemResponse) {
        this.selectedChatSource.next(chat);
    }

    GetConversationsAsync(): Observable<ApiResponse<ChatItemResponse[]>> {
        return this.http.get<ApiResponse<ChatItemResponse[]>>(
            `${this.apiUrl}/conversations`
        );
    }
}

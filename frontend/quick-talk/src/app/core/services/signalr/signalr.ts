import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { map, Subject } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class Signalr {
    private hubConnection!: signalR.HubConnection;

    //stream messages to components
    private messageSubject = new Subject<any>();
    public message$ = this.messageSubject.asObservable();
    public userStatuses = new Map<string, string>();
    public userLastSeen = new Map<string, string>();

    startConnection(token: string) {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:44371/chatHub', {
                accessTokenFactory: () => token
            })
            .withAutomaticReconnect()
            .build();

        this.hubConnection
            .start()
            .then(() => console.log('SignalR Connected.'))
            .catch(err => console.log('SignalR Error:', err));

        //listen from backend
        this.hubConnection.on('ReceiveMessage', (senderId, message) => {
            console.log('🔥 MESSAGE RECEIVED', senderId, message);
            this.messageSubject.next({ senderId, message });
        });

        this.hubConnection.on('OnlineUsers', (users: string[]) => {
            users.forEach(userId => {
                this.userStatuses.set(userId, 'Online');
            });
        });

        this.hubConnection.on('UserStatusChanged', (userId: string, status: string) => {
            this.userStatuses.set(userId, status);
        })
        console.log('status', this.userStatuses);
    }

    getStatus(userId: string) {
        return this.userStatuses.get(userId) ?? 'Offline';
    }

    setLastSeen(userId: string, lastSeen: string | null) {
        if (lastSeen) {
            this.userLastSeen.set(userId, lastSeen);
        }
    }

    getLastSeen(userId: string) {
        return this.userLastSeen.get(userId);
    }

    stopConnection() {
        this.hubConnection.stop();
    }
}

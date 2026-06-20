import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class Signalr {
    private hubConnection!: signalR.HubConnection;

    //stream messages to components
    private messageSubject = new Subject<any>();
    public message$ = this.messageSubject.asObservable();

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
    }

    stopConnection() {
        this.hubConnection.stop();
    }
}

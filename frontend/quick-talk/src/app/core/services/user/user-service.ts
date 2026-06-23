import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

@Injectable({
    providedIn: 'root',
})
export class UserService {
    private http = inject(HttpClient);
    private apiUrl = `${environment.apiUrl}/user`;

    getLastSeen(userId: number) {
        return this.http.get<string | null>(
            `${this.apiUrl}/${userId}/last-seen`
        );
    }
}

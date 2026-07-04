import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/apiResponse/api-response';
import { UserDetails } from '../../models/user/user-details';

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

    getPeopleToChat(): Observable<ApiResponse<UserDetails[]>> {
        return this.http.get<ApiResponse<UserDetails[]>>(
            `${this.apiUrl}/discover`
        );
    }
}

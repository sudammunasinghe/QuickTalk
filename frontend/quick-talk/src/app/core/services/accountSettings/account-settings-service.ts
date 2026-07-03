import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { ChangePasswordRequest } from '../../models/account/change-password-request';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/apiResponse/api-response';
import { PrivacySettingsResponse } from '../../models/account/privacy-settings';

@Injectable({
    providedIn: 'root',
})
export class AccountSettingsService {
    private http = inject(HttpClient);
    private apiUrl = `${environment.apiUrl}/accountSettings`;

    ChangePasswordAsync(request: ChangePasswordRequest): Observable<ApiResponse<string>> {
        return this.http.post<ApiResponse<string>>(
            `${this.apiUrl}/change-password`,
            request
        );
    }

    GetPrivacySettingDetailsAsync(): Observable<ApiResponse<PrivacySettingsResponse>> {
        return this.http.get<ApiResponse<PrivacySettingsResponse>>(
            `${this.apiUrl}/privacy-settings`
        )
    }
}

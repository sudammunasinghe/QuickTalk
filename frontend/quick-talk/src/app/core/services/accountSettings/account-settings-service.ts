import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { ChangePasswordRequest } from '../../models/account/change-password-request';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/apiResponse/api-response';
import { PrivacySettingsResponse } from '../../models/account/privacy-settings';
import { PrivacySettings } from '../../../features/chat/components/sidebar-header/account-settings-dialog/privacy-settings/privacy-settings';
import { PrivacySettingsRequest } from '../../models/account/privacy-settings-request';

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

    UpdatePrivacySettingsAsync(request: PrivacySettingsRequest): Observable<ApiResponse<PrivacySettingsResponse>> {
        return this.http.put<ApiResponse<PrivacySettingsResponse>>(
            `${this.apiUrl}/privacy-settings`,
            request
        )
    }
}

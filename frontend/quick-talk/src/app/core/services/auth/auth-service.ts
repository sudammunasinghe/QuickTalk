import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoginRequest } from '../../models/auth/login-request';
import { empty, Observable } from 'rxjs';
import { ApiResponse } from '../../models/apiResponse/api-response';
import { environment } from './../../../../environments/environment';
import { RegisterRequest } from '../../models/auth/register-request';
import { ResetPassword } from '../../../features/auth/reset-password/reset-password';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private http = inject(HttpClient);
    private apiUrl = `${environment.apiUrl}/auth`;

    signIn(request: LoginRequest): Observable<ApiResponse<string>> {
        return this.http.post<ApiResponse<string>>(
            `${this.apiUrl}/login`,
            request
        );
    }

    signUp(request: RegisterRequest): Observable<ApiResponse<string>>{
        return this.http.post<ApiResponse<string>>(
            `${this.apiUrl}/register`,
            request
        );
    }

    forgotPassword(email: string): Observable<ApiResponse<string>>{
        return this.http.post<ApiResponse<string>>(
            `${this.apiUrl}/forgot-password`,
            email
        );
    }

    resetPassword(request: ResetPassword): Observable<ApiResponse<string>>{
        return this.http.post<ApiResponse<string>>(
            `${this.apiUrl}/reset-password`,
            request
        )
    }

}

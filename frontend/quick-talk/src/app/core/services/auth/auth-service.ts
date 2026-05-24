import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoginRequest } from '../../models/auth/login-request';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/apiResponse/api-response';
import { environment } from './../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/auth`;

  signIn(request: LoginRequest): Observable<ApiResponse<string>>{
    return this.http.post<ApiResponse<string>>(
      `${this.apiUrl}/login`,
      request
    );
  }
}

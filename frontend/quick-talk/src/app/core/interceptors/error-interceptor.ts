import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { TokenService } from '../services/token/token-service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
    const router = inject(Router);
    const tokenService = inject(TokenService);

    return next(req).pipe(
        catchError(error => {
            if (error.status === 401) {
                tokenService.removeToken();
                router.navigate(['/sign-in']);
            }
            return throwError(() => error);
        })
    );
};

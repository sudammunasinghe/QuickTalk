import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { CardModule } from 'primeng/card';
import { PasswordModule } from 'primeng/password';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth/auth-service';
import {
    ReactiveFormsModule,
    FormBuilder,
    Validators,
    FormGroup
} from '@angular/forms';

@Component({
    selector: 'app-forgot-password',
    imports: [
        RouterLink,
        ReactiveFormsModule,
        ButtonModule,
        InputTextModule,
        CardModule,
        PasswordModule,
        ToastModule
    ],
    // providers: [MessageService],
    templateUrl: './forgot-password.html',
    styleUrl: './forgot-password.scss',
})
export class ForgotPassword {
    forgotPasswordForm!: FormGroup;
    isLoading = false;

    constructor(
        private fb: FormBuilder,
        private messageService: MessageService,
        private authService: AuthService,
        private router: Router
    ) {
        this.forgotPasswordForm = this.fb.group({
            email: ['', [Validators.required, Validators.email]]
        });
    }

    sendResetLink() {
        if (this.forgotPasswordForm.invalid)
            return;
        this.isLoading = true;
        this.authService.forgotPassword(this.forgotPasswordForm.value)
            .subscribe({
                next: (response) => {
                    this.isLoading = false;
                    if (response.isSuccess) {
                        this.messageService.add({
                            severity: 'success',
                            summary: 'success',
                            detail: response.message
                        });
                        this.router.navigate(['/reset-password'], {
                            queryParams: {
                                email: this.forgotPasswordForm.value.email
                            }
                        });
                    }
                },
                error: (response) => {
                    this.isLoading = false;
                    this.messageService.add({
                        severity: 'error',
                        summary: 'error',
                        detail: response.error.Message
                    });
                }
            });
    }
}

import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ToastModule } from 'primeng/toast';
import { InputOtpModule } from 'primeng/inputotp';
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
    selector: 'app-reset-password',
    imports: [
        ButtonModule,
        CardModule,
        InputTextModule,
        PasswordModule,
        ToastModule,
        ReactiveFormsModule,
        RouterLink,
        InputOtpModule
    ],
    providers: [MessageService],
    templateUrl: './reset-password.html',
    styleUrl: './reset-password.scss',
})
export class ResetPassword {
    resetPasswordForm!: FormGroup;
    isLoading = false;

    constructor(
        private fb: FormBuilder,
        private authservice: AuthService,
        private messageService: MessageService
    ) {
        this.resetPasswordForm = this.fb.group({
            newPassword: ['', Validators.required],
            confirmNewPassword: ['', Validators.required],
            otpValue: ['']
        });
    }

    resetPassword() {
        if (this.resetPasswordForm.invalid)
            return;
        console.log(this.resetPasswordForm.value);
        this.isLoading = true;
        this.authservice.resetPassword(this.resetPasswordForm.value)
            .subscribe({
                next: (response) => {
                    this.isLoading = false;
                    if (response.isSuccess) {
                        this.messageService.add({
                            severity: 'success',
                            summary: 'success',
                            detail: response.message
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

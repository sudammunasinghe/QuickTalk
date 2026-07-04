import { Component, EventEmitter, Output } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { CardModule } from 'primeng/card';
import { PasswordModule } from 'primeng/password';
import { AccountSettingsService } from '../../../../../../core/services/accountSettings/account-settings-service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { ChangePasswordRequest } from '../../../../../../core/models/account/change-password-request';

@Component({
    selector: 'app-password-settings',
    imports: [
        ButtonModule,
        InputTextModule,
        CardModule,
        PasswordModule,
        ReactiveFormsModule
    ],
    templateUrl: './password-settings.html',
    styleUrl: './password-settings.scss',
})
export class PasswordSettings {
    @Output() close = new EventEmitter<void>();
    changePasswordForm!: FormGroup;
    constructor(
        private settingsService: AccountSettingsService,
        private fb: FormBuilder,
        private messageService: MessageService
    ) {
        this.changePasswordForm = this.fb.group({
            currentPassword: ['', [Validators.required]],
            newPassword: ['', [Validators.required]],
            confirmNewPassword: ['', [Validators.required]]
        });
    }

    changePassword(): void {
        if (this.changePasswordForm.invalid)
            return;

        const changePasswordRequest: ChangePasswordRequest = {
            currentPassword: this.changePasswordForm.value.currentPassword,
            newPassword: this.changePasswordForm.value.newPassword,
            confirmedNewPassword: this.changePasswordForm.value.confirmNewPassword
        };

        this.settingsService.ChangePasswordAsync(changePasswordRequest)
            .subscribe({
                next: (response) => {
                    this.changePasswordForm.reset();
                    if (response.isSuccess) {
                        this.close.emit();
                        this.messageService.add({
                            severity: 'success',
                            summary: 'success',
                            detail: response.message
                        });
                    }
                },
                error: (response) => {
                    this.changePasswordForm.reset();
                    this.messageService.add({
                        severity: 'error',
                        summary: 'error',
                        detail: response.error.Message
                    });
                }
            })
    }
}


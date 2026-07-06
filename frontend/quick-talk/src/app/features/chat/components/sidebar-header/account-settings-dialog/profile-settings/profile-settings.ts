import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { TextareaModule } from 'primeng/textarea';
import { FileUploadModule } from 'primeng/fileupload';
import { AccountSettingsService } from '../../../../../../core/services/accountSettings/account-settings-service';
import { ProfileDetailsResponse } from '../../../../../../core/models/account/profile-details-response';
import { MessageService } from 'primeng/api';

@Component({
    selector: 'app-profile-settings',
    imports: [
        CommonModule,
        ButtonModule,
        TextareaModule,
        InputTextModule,
        PasswordModule,
        ReactiveFormsModule,
        FileUploadModule
    ],
    templateUrl: './profile-settings.html',
    styleUrl: './profile-settings.scss',
})
export class ProfileSettings {
    defaultImage = 'assets/images/profile.png';
    profileDetails!: ProfileDetailsResponse;
    profileForm!: FormGroup;

    constructor(
        private fb: FormBuilder,
        private accountService: AccountSettingsService,
        private messageService: MessageService
    ) { }

    ngOnInit() {
        this.loadProfileDetails();
        this.initializeForm();
    }

    initializeForm(): void {
        this.profileForm = this.fb.group({
            firstName: [''],
            lastName: [''],
            bio: [''],
            dateOfBirth: ['']
        });
    }

    loadProfileDetails(): void {
        this.accountService.GetProfileDetailsAsync()
            .subscribe({
                next: (response) => {
                    if (response.isSuccess && response.data) {
                        this.profileDetails = response.data;
                        this.setInitialFormData();
                    }
                },
                error: (response) => {
                    this.messageService.add({
                        severity: 'error',
                        summary: 'error',
                        detail: response.error.Message
                    });
                }
            });
    }

    setInitialFormData(): void {
        this.profileForm.patchValue({
            firstName: this.profileDetails.firstName,
            lastName: this.profileDetails.lastName,
            bio: this.profileDetails.bio,
            dateOfBirth: this.profileDetails.dateOfBirth ? this.profileDetails.dateOfBirth.split('T')[0] : ''
        });
    }
}

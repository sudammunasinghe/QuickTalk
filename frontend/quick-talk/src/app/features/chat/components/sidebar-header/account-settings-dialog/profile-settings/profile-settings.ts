import { CommonModule } from '@angular/common';
import { Component, ChangeDetectorRef, Input, OnChanges } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { TextareaModule } from 'primeng/textarea';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { FileUploadModule } from 'primeng/fileupload';
import { AccountSettingsService } from '../../../../../../core/services/accountSettings/account-settings-service';
import { ProfileDetailsResponse } from '../../../../../../core/models/account/profile-details-response';
import { MessageService, ConfirmationService } from 'primeng/api';

@Component({
    selector: 'app-profile-settings',
    standalone: true,
    imports: [
        CommonModule,
        ButtonModule,
        TextareaModule,
        InputTextModule,
        PasswordModule,
        ReactiveFormsModule,
        FileUploadModule,
        ConfirmDialogModule
    ],
    templateUrl: './profile-settings.html',
    providers: [ConfirmationService],
    styleUrl: './profile-settings.scss',
})
export class ProfileSettings {
    @Input() profileDetails!: ProfileDetailsResponse;
    defaultAvator = 'assets/images/profile.png';
    imagePreview = this.defaultAvator;
    profileForm!: FormGroup;
    selectedFile: File | null = null;
    hasProfilePhoto!: boolean;
    isProfileImageRemoved = false;

    constructor(
        private fb: FormBuilder,
        private accountService: AccountSettingsService,
        private messageService: MessageService,
        private cdr: ChangeDetectorRef,
        private confirmationService: ConfirmationService
    ) { }

    ngOnChanges() {
        this.initializeForm();
        this.imagePreview = this.profileDetails?.profilePictureUrl || this.defaultAvator;
        this.hasProfilePhoto = this.profileDetails.profilePictureUrl ? true : false;
        this.setInitialFormData();
    }

    initializeForm(): void {
        this.profileForm = this.fb.group({
            firstName: [''],
            lastName: [''],
            bio: [''],
            dateOfBirth: ['']
        });
    }

    SaveProfileSettings(): void {
        if (this.profileForm.invalid)
            return;

        const formData = new FormData();
        formData.append('FirstName', this.profileForm?.value?.firstName);
        formData.append('LastName', this.profileForm?.value?.lastName);
        formData.append('Bio', this.profileForm?.value?.bio);
        formData.append('DateOfBirth', this.profileForm?.value?.dateOfBirth);
        formData.append('RemoveProfileImage', this.isProfileImageRemoved ? 'true' : 'false');
        if (this.selectedFile) {
            formData.append('ProfilePicture', this.selectedFile);
        }

        this.accountService.UpdateProfileDetailsAsync(formData)
            .subscribe({
                next: (resonse) => {
                    if (resonse.isSuccess && resonse.data) {
                        this.profileDetails = resonse.data;
                        this.imagePreview = this.profileDetails?.profilePictureUrl || this.defaultAvator;
                        this.hasProfilePhoto = this.profileDetails.profilePictureUrl ? true : false;
                        this.setInitialFormData();
                        this.messageService.add({
                            severity: 'success',
                            summary: 'success',
                            detail: resonse.message
                        });
                    }
                },
                error: (response) => {
                    console.log('error',response);
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
            dateOfBirth: this.profileDetails.dateOfBirth ? this.profileDetails.dateOfBirth.split('T')[0] : '',
        });
    }

    onImageSelect(event: any): void {
        const input = event.target as HTMLInputElement;
        if (!input.files?.length)
            return;

        this.selectedFile = input.files[0];
        const reader = new FileReader();
        reader.onload = () => {
            this.imagePreview = reader.result as string;
            this.hasProfilePhoto = true;
            this.cdr.detectChanges();
        };
        reader.readAsDataURL(this.selectedFile);
    }

    removeProfilePicture(): void {
        this.isProfileImageRemoved = true;
        this.imagePreview = this.defaultAvator;
        this.hasProfilePhoto = false;
        this.selectedFile = null;
    }

    confirmDelete(): void {
        this.confirmationService.confirm({
            message: 'Are you sure you want to delete your profile picture?',
            header: 'Delete Profile Picture',
            icon: 'pi pi-exclamation-triangle',
            rejectLabel: 'Cancel',
            rejectButtonProps: {
                label: 'Cancel',
                severity: 'secondary',
                outlined: true
            },
            acceptButtonProps: {
                label: 'Delete',
                severity: 'danger'
            },

            accept: () => {
                this.removeProfilePicture();
            }
        });
    }

    getInitials(): string {
        return (
            (this.profileDetails?.firstName?.charAt(0) ?? '') +
            (this.profileDetails?.lastName?.charAt(0) ?? '')
        ).toUpperCase();
    }
}


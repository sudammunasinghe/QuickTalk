import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { PrivacySettingsResponse } from '../../../../../../core/models/account/privacy-settings';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AccountSettingsService } from '../../../../../../core/services/accountSettings/account-settings-service';
import { PrivacySettingsRequest } from '../../../../../../core/models/account/privacy-settings-request';
import { PrivacyOptions } from '../../../../../../core/enums/privacy-options';
import { MessageService } from 'primeng/api';

@Component({
    selector: 'app-privacy-settings',
    standalone: true,
    imports: [
        CardModule,
        ButtonModule,
        ToggleSwitchModule,
        FormsModule,
        CommonModule
    ],
    templateUrl: './privacy-settings.html',
    styleUrl: './privacy-settings.scss',
})
export class PrivacySettings {
    @Input() privacySettingData!: PrivacySettingsResponse;
    @Output() close = new EventEmitter<void>();

    privacySettings!: PrivacySettingsResponse;
    privacies: any[] = [];
    constructor(
        private settingsService: AccountSettingsService,
        private messageService: MessageService
    ) { }

    ngOnInit() {
        this.privacies = [
            {
                key: PrivacyOptions.ShowProfile,
                label: 'Profile Picture',
                description: 'Allow others to see your profile photo',
                icon: 'pi pi-user',
                value: this.privacySettingData?.showProfilePicture
            },
            {
                key: PrivacyOptions.ShowOnlineStatus,
                label: 'Online Status',
                description: 'Show when you are currently active',
                icon: 'pi pi-circle-fill',
                value: this.privacySettingData?.showOnlineStatus
            },
            {
                key: PrivacyOptions.ShowLastSeen,
                label: 'Last Seen',
                description: 'Display your last active time to others',
                icon: 'pi pi-clock',
                value: this.privacySettingData?.showLastSeen
            },
            {
                key: PrivacyOptions.ShowBio,
                label: 'Bio & Info',
                description: 'Show your bio and personal info on your profile',
                icon: 'pi pi-book',
                value: this.privacySettingData?.showBio
            },
        ];
    }

    savePrivacySettings(): void {
        const request: PrivacySettingsRequest = {
            showProfilePicture: this.privacies.find(x => x.key === PrivacyOptions.ShowProfile)?.value,
            showOnlineStatus: this.privacies.find(x => x.key === PrivacyOptions.ShowOnlineStatus)?.value,
            showLastSeen: this.privacies.find(x => x.key === PrivacyOptions.ShowLastSeen)?.value,
            showBio: this.privacies.find(x => x.key === PrivacyOptions.ShowBio)?.value
        }

        this.settingsService.UpdatePrivacySettingsAsync(request)
            .subscribe({
                next: (response) => {
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
                    this.messageService.add({
                        severity: 'error',
                        summary: 'error',
                        detail: response.error.Message
                    });
                }
            });
    }
}

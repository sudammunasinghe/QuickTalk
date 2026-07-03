import { Component, Input, OnInit } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { PrivacySettingsResponse } from '../../../../../../core/models/account/privacy-settings';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AccountSettingsService } from '../../../../../../core/services/accountSettings/account-settings-service';
import { delay } from 'rxjs';

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
    @Input() privacySettingData!: PrivacySettingsResponse
    privacySettings!: PrivacySettingsResponse;
    privacies: any[] = [];

    constructor(
        private settingsService: AccountSettingsService
    ) { }

    ngOnInit() {
        this.privacies = [
            {
                label: 'Profile Picture',
                description: 'Allow others to see your profile photo',
                icon: 'pi pi-user',
                value: this.privacySettingData?.showProfilePicture
            },
            {
                label: 'Online Status',
                description: 'Show when you are currently active',
                icon: 'pi pi-circle-fill',
                value: this.privacySettingData?.showOnlineStatus
            },
            {
                label: 'Last Seen',
                description: 'Display your last active time to others',
                icon: 'pi pi-clock',
                value: this.privacySettingData?.showLastSeen
            },
            {
                label: 'Bio & Info',
                description: 'Show your bio and personal info on your profile',
                icon: 'pi pi-book',
                value: this.privacySettingData?.showBio
            },
        ];
    }
}

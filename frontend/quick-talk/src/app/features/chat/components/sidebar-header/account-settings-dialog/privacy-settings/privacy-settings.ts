import { Component, OnInit } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { PrivacySettingsResponse } from '../../../../../../core/models/account/privacy-settings';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-privacy-settings',
    imports: [
        CardModule,
        ButtonModule,
        ToggleSwitchModule,
        CommonModule
    ],
    templateUrl: './privacy-settings.html',
    styleUrl: './privacy-settings.scss',
})
export class PrivacySettings {
    privacySettings!: PrivacySettingsResponse;
    privacies: any[] = [];

    ngOnInit() {
        this.privacySettings = {
            id: 100,
            showProfilePicture: true,
            showOnlineStatus: true,
            showLastSeen: true,
            showBio: true
        }
        this.privacies = [
            {
                label: 'Profile Picture',
                description: 'Allow others to see your profile photo',
                icon: 'pi pi-user',
                value: this.privacySettings.showProfilePicture
            },
            {
                label: 'Online Status',
                description: 'Show when you are currently active',
                icon: 'pi pi-circle-fill',
                value: this.privacySettings.showOnlineStatus
            },
            {
                label: 'Last Seen',
                description: 'Display your last active time to others',
                icon: 'pi pi-clock',
                value: this.privacySettings.showLastSeen
            },
            {
                label: 'Bio & Info',
                description: 'Show your bio and personal info on your profile',
                icon: 'pi pi-book',
                value: this.privacySettings.showBio
            },

        ];
    }
}

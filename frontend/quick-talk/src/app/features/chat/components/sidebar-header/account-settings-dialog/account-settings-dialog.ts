import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { SettingsTab } from '../../../../../core/enums/settings-tab';
import { ProfileSettings } from './profile-settings/profile-settings';
import { PasswordSettings } from './password-settings/password-settings';
import { PrivacySettings } from './privacy-settings/privacy-settings';
import { PrivacySettingsResponse } from '../../../../../core/models/account/privacy-settings';
import { AccountSettingsService } from '../../../../../core/services/accountSettings/account-settings-service';

@Component({
    selector: 'app-account-settings-dialog',
    standalone: true,
    imports: [
        CommonModule,
        ProfileSettings,
        PasswordSettings,
        PrivacySettings
    ],
    templateUrl: './account-settings-dialog.html',
    styleUrl: './account-settings-dialog.scss',
})
export class AccountSettingsDialog {
    @Output() close = new EventEmitter<void>();
    privacySettings!: PrivacySettingsResponse;
    readonly SettingsTab = SettingsTab;

    constructor(
        private settingsService: AccountSettingsService
    ) { }

    tabs = [
        {
            label: 'Profile',
            description: 'Name, photo & birthday',
            icon: 'pi pi-user',
            value: SettingsTab.Profile
        },
        {
            label: 'Password',
            description: 'Change your password',
            icon: 'pi pi-lock',
            value: SettingsTab.Password
        },
        {
            label: 'Privacy',
            description: 'Visibility & data control',
            icon: 'pi pi-shield',
            value: SettingsTab.Privacy
        }
    ];
    selectedTab = SettingsTab.Profile;

    ngOnInit() {
        this.loadPrivacySettingsAsync();
    }

    closeDialog(): void {
        this.close.emit();
    }

    loadPrivacySettingsAsync(): void {
        this.settingsService.GetPrivacySettingDetailsAsync()
            .subscribe({
                next: (response) => {
                    if (response.isSuccess && response.data) {
                        this.privacySettings = response.data
                    }
                }
            })
    }
}

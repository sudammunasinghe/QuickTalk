import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { SettingsTab } from '../../../../../core/enums/settings-tab';
import { ProfileSettings } from './profile-settings/profile-settings';
import { PasswordSettings } from './password-settings/password-settings';
import { PrivacySettings } from './privacy-settings/privacy-settings';
import { PrivacySettingsResponse } from '../../../../../core/models/account/privacy-settings';
import { AccountSettingsService } from '../../../../../core/services/accountSettings/account-settings-service';
import { ButtonModule } from 'primeng/button';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { Signalr } from '../../../../../core/services/signalr/signalr';
import { TokenService } from '../../../../../core/services/token/token-service';
import { Router, RouterLink } from '@angular/router';
import { ProfileDetailsResponse } from '../../../../../core/models/account/profile-details-response';

@Component({
    selector: 'app-account-settings-dialog',
    standalone: true,
    imports: [
        CommonModule,
        ProfileSettings,
        PasswordSettings,
        PrivacySettings,
        ButtonModule,
        ConfirmDialogModule,
        RouterLink
    ],
    templateUrl: './account-settings-dialog.html',
    providers: [ConfirmationService],
    styleUrl: './account-settings-dialog.scss',
})
export class AccountSettingsDialog {
    @Output() close = new EventEmitter<void>();
    privacySettings!: PrivacySettingsResponse;
    profileSettingsDetails!: ProfileDetailsResponse;
    readonly SettingsTab = SettingsTab;

    constructor(
        private settingsService: AccountSettingsService,
        private confirmationService: ConfirmationService,
        private messageService: MessageService,
        private signalrService: Signalr,
        private tokenService: TokenService,
        private router: Router
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
        this.loadProfileDetailsAsync();
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
                },
                error: (response) => {
                    this.messageService.add({
                        severity: 'error',
                        summary: 'error',
                        detail: response.error.Message
                    });
                }
            })
    }

    loadProfileDetailsAsync():void{
        this.settingsService.GetProfileDetailsAsync()
            .subscribe({
                next: (response) => {
                    if(response.isSuccess && response.data){
                        this.profileSettingsDetails = response.data;
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

    confirmLogout(): void {
        this.confirmationService.confirm({
            message: 'Are you sure you want to log out?',
            header: 'Logout',
            icon: 'pi pi-exclamation-circle',
            rejectLabel: 'Cancel',
            rejectButtonProps: {
                label: 'No',
                severity: 'secondary',
                outlined: true
            },
            acceptButtonProps: {
                label: 'Yes',
                severity: 'danger'
            },
            accept: () => {
                this.logout();
            }
        });
    }

    logout(): void {
        this.signalrService.stopConnection();
        this.tokenService.removeToken();
        this.router.navigate(['/sign-in']);
    }
}

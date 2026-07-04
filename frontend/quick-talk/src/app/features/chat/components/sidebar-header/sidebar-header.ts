import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output, output } from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { FindPeopleDialog } from '../sidebar-header/find-people-dialog/find-people-dialog';
import { AccountSettingsDialog } from './account-settings-dialog/account-settings-dialog';

@Component({
    selector: 'app-sidebar-header',
    standalone: true,
    imports: [
        DialogModule,
        CommonModule,
        FindPeopleDialog,
        AccountSettingsDialog
    ],
    templateUrl: './sidebar-header.html',
    styleUrl: './sidebar-header.scss',
})
export class SidebarHeader {
    @Output() searchChanged = new EventEmitter<string>();
    logoUrl = 'assets/images/logo.png';
    displayFindPeopleDialog = false;
    displaySettingsDialog = false;

    showFindPeopleDialog(): void {
        this.displayFindPeopleDialog = true;
    }

    showSettingsDialog(): void{
        this.displaySettingsDialog = true;
    }

    onSearch(event: Event) {
        const value = (event.target as HTMLInputElement).value;
        this.searchChanged.emit(value);
    }
}

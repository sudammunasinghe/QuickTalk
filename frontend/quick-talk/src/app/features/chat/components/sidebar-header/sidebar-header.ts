import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { FindPeopleDialog } from '../sidebar-header/find-people-dialog/find-people-dialog';

@Component({
    selector: 'app-sidebar-header',
    standalone: true,
    imports: [
        DialogModule,
        CommonModule,
        FindPeopleDialog
    ],
    templateUrl: './sidebar-header.html',
    styleUrl: './sidebar-header.scss',
})
export class SidebarHeader {
    logoUrl = 'assets/images/logo.png';
    displayFindPeopleDialog = false;

    showFindPeopleDialog(): void {
        this.displayFindPeopleDialog = true;
    }
}

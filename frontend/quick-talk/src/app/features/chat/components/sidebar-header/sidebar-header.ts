import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
    selector: 'app-sidebar-header',
    standalone: true,
    imports: [
        CommonModule
    ],
    templateUrl: './sidebar-header.html',
    styleUrl: './sidebar-header.scss',
})
export class SidebarHeader {
    logoUrl = 'assets/images/logo.png';
}

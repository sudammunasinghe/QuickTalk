import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
    selector: 'app-sidebar-header',
    imports: [],
    templateUrl: './sidebar-header.html',
    styleUrl: './sidebar-header.scss',
})
export class SidebarHeader {
    logoUrl = 'assets/images/logo.png';
}

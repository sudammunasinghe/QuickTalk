import { Component, Input } from '@angular/core';
import { Signalr } from '../../../../../../core/services/signalr/signalr';
import { CommonModule } from '@angular/common';
import { UserDetails } from '../../../../../../core/models/user/user-details';

@Component({
    selector: 'app-user-item',
    imports: [
        CommonModule
    ],
    templateUrl: './user-item.html',
    styleUrl: './user-item.scss',
})
export class UserItem {
    @Input() user!: UserDetails;
    constructor(
        private realTimeService: Signalr
    ) { }

    getInitials(): string {
        return (
            (this.user?.firstName?.charAt(0) ?? '') +
            (this.user?.lastName.charAt(0) ?? '')
        ).toUpperCase();
    }

    getStatusColor(userId: number): string {
        const status = this.realTimeService.getStatus(userId.toString());
        if (status == 'Online') return '#22c55e';
        if (status == 'Away') return '#f59e0b';
        return '#64748b';
    }

    getStatusText(userId: number): string {
        const status = this.realTimeService.getStatus(userId.toString());
        return status;
    }
}

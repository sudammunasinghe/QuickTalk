import { Component } from '@angular/core';
import { UserItem } from '../find-people-dialog/user-item/user-item';
import { UserService } from '../../../../../core/services/user/user-service';
import { UserDetails } from '../../../../../core/models/user/user-details';
import { CommonModule } from '@angular/common';
import { MessageService } from 'primeng/api';

@Component({
    selector: 'app-find-people-dialog',
    standalone: true,
    imports: [
        UserItem,
        CommonModule
    ],
    templateUrl: './find-people-dialog.html',
    styleUrl: './find-people-dialog.scss',
})
export class FindPeopleDialog {
    discoveredPeople!: UserDetails[];
    constructor(
        private userService: UserService,
        private messageService: MessageService
    ) { }

    ngOnInit() {
        this.loadPeopleToChat();
    }

    loadPeopleToChat(): void {
        this.userService.getPeopleToChat()
            .subscribe({
                next: (response) => {
                    if (response.isSuccess && response.data) {
                        this.discoveredPeople = response.data;
                        console.log('sudam', this.discoveredPeople);
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
}

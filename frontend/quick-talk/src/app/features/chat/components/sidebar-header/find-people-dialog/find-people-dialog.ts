import { Component, EventEmitter, Output } from '@angular/core';
import { UserItem } from '../find-people-dialog/user-item/user-item';
import { UserService } from '../../../../../core/services/user/user-service';
import { CommonModule } from '@angular/common';
import { MessageService } from 'primeng/api';
import { ChatItemResponse } from '../../../../../core/models/chat/chat-item-response';

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
    @Output() close = new EventEmitter<void>();
    discoveredPeople!: ChatItemResponse[];
    filteredPeople!: ChatItemResponse[];
    peopleCount = 0;
    constructor(
        private userService: UserService,
        private messageService: MessageService
    ) { }

    ngOnInit() {
        this.loadPeopleToChat();
    }

    closeDialog(): void {
        this.close.emit();
    }

    loadPeopleToChat(): void {
        this.userService.getPeopleToChat()
            .subscribe({
                next: (response) => {
                    if (response.isSuccess && response.data) {
                        this.discoveredPeople = response.data;
                        this.filteredPeople = this.discoveredPeople;
                        this.peopleCount = this.filteredPeople.length;
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

    onSearch(event: Event): void {
        const value = (event.target as HTMLInputElement).value;
        this.filterPeople(value);
    }

    filterPeople(searchValue: string): void {
        if (!searchValue.trim()) {
            this.filteredPeople = [...this.discoveredPeople];
            return;
        }

        const value = searchValue.toLowerCase();
        this.filteredPeople = this.discoveredPeople.filter(people =>
            (`${people.firstName} ${people.lastName}`)
                .toLowerCase()
                .includes(value)
        );
        this.peopleCount = this.filteredPeople.length;
    }
}

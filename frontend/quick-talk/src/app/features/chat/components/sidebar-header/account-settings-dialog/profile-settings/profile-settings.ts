import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { TextareaModule } from 'primeng/textarea';
import { FileUploadModule } from 'primeng/fileupload';

@Component({
    selector: 'app-profile-settings',
    imports: [
        CommonModule,
        ButtonModule,
        TextareaModule,
        InputTextModule,
        PasswordModule,
        ReactiveFormsModule,
        FileUploadModule
    ],
    templateUrl: './profile-settings.html',
    styleUrl: './profile-settings.scss',
})
export class ProfileSettings { 
    defaultImage = 'assets/images/profile.png'
}

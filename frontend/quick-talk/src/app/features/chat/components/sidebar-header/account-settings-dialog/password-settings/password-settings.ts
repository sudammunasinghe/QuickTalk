import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { CardModule } from 'primeng/card';
import { PasswordModule } from 'primeng/password';

@Component({
  selector: 'app-password-settings',
  imports: [
    ButtonModule,
    InputTextModule,
    CardModule,
    PasswordModule
  ],
  templateUrl: './password-settings.html',
  styleUrl: './password-settings.scss',
})
export class PasswordSettings {}

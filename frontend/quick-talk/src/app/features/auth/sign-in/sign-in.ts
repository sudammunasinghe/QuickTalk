import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password'
import { CardModule } from 'primeng/card';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import {
    ReactiveFormsModule,
    FormBuilder,
    Validators,
    FormGroup
} from '@angular/forms';
import { AuthService } from '../../../core/services/auth/auth-service';
import { concatWith } from 'rxjs';

@Component({
    selector: 'app-sign-in',
    imports: [
        RouterLink,
        ReactiveFormsModule,
        ButtonModule,
        InputTextModule,
        PasswordModule,
        CardModule,
        ToastModule
    ],
    // providers: [MessageService],
    templateUrl: './sign-in.html',
    styleUrl: './sign-in.scss',
})
export class SignIn {
    signInForm!: FormGroup;
    isLoading = false;

    constructor(
        private fb: FormBuilder,
        private authservice: AuthService,
        private router: Router,
        private messageService: MessageService
    ) {
        this.signInForm = this.fb.group({
            email: ['', [Validators.required, Validators.email]],
            password: ['', [Validators.required]]
        });
    }

    onSubmit() {
        if (this.signInForm.invalid) {
            return;
        }
        this.isLoading = true;
        this.authservice.signIn(this.signInForm.value)
            .subscribe({
                next: (response) => {
                    this.isLoading = false;
                    if (response.isSuccess) {
                        localStorage.setItem('token', response.data);
                        this.messageService.add({
                            severity: 'success',
                            summary: 'success',
                            detail: response.message
                        });
                        this.router.navigate(['/chat']);
                    }
                },
                error: (response) => {
                    this.isLoading = false;
                    this.messageService.add({
                        severity: 'error',
                        summary: 'error',
                        detail: response.error.Message
                    });
                }
            });
    }
}

import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { CardModule } from 'primeng/card';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { AuthService } from '../../../core/services/auth/auth-service';
import {
    ReactiveFormsModule,
    FormBuilder,
    Validators,
    FormGroup
} from '@angular/forms'

@Component({
    selector: 'app-sign-up',
    imports: [
        ButtonModule,
        InputTextModule,
        PasswordModule,
        CardModule,
        ToastModule,
        ReactiveFormsModule,
        RouterLink
    ],
    providers: [MessageService],
    templateUrl: './sign-up.html',
    styleUrl: './sign-up.scss',
})
export class SignUp { 
    signUpForm!: FormGroup;
    isLoading = false;

    constructor(
        private fb: FormBuilder,
        private authService: AuthService,
        private messageService: MessageService,
        private router: Router
    ){
        this.signUpForm = this.fb.group({
            firstName: ['',[Validators.required]],
            lastName: ['',[Validators.required]],
            email: ['',[Validators.required, Validators.email]],
            dateOfBirth: ['',[Validators.required]],
            password: ['',[Validators.required]],
            confirmPassword: ['',[Validators.required]]
        });
    };

    onSubmit(){
        if(this.signUpForm.invalid)
            return;
        this.isLoading = true;
        this.authService.signUp(this.signUpForm.value)
            .subscribe({
                next: (response) => {
                    this.isLoading = false;
                    if(response.isSuccess){
                        this.messageService.add({
                            severity: 'success',
                            summary: 'success',
                            detail: response.message
                        });
                        this.router.navigate(['/sign-in']);
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

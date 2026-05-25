import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ToastModule } from 'primeng/toast';
import { InputOtpModule } from 'primeng/inputotp';
import { MessageService } from 'primeng/api';
import { Router, RouterLink } from '@angular/router';
import {
    ReactiveFormsModule,
    FormBuilder,
    Validators,
    FormGroup
} from '@angular/forms';


@Component({
    selector: 'app-reset-password',
    imports: [
        ButtonModule,
        CardModule,
        InputTextModule,
        PasswordModule,
        ToastModule,
        ReactiveFormsModule,
        RouterLink,
        InputOtpModule
    ],
    templateUrl: './reset-password.html',
    styleUrl: './reset-password.scss',
})
export class ResetPassword {
    resetPasswordForm!: FormGroup;
    
    constructor(
        private fb: FormBuilder
    ){
        this.resetPasswordForm = this.fb.group({
            newPassword: ['',Validators.required],
            confirmNewPassword: ['',Validators.required],
            otpValue: ['']
        });
    }

    resetPassword(){
        if(this.resetPasswordForm.invalid)
            return;
        console.log(this.resetPasswordForm.value);
    }
}

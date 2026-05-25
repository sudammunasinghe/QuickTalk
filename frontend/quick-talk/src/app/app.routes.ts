import { Routes } from '@angular/router';
import { AuthLayout } from './layouts/auth-layout/auth-layout';
import { SignIn } from './features/auth/sign-in/sign-in';
import { SignUp } from './features/auth/sign-up/sign-up';
import { ForgotPassword } from './features/auth/forgot-password/forgot-password';
import { MainLayout } from './layouts/main-layout/main-layout';
import { ChatLayout } from './features/chat/chat-layout/chat-layout';

export const routes: Routes = [
    {
        path: '',
        component: AuthLayout,
        children: [
            {
                path: '',
                redirectTo: 'sign-up',
                pathMatch: 'full'
            },
            {
                path: 'sign-up',
                component: SignUp
            },
            {
                path: 'sign-in',
                component: SignIn
            },
            {
                path: 'forgot-password',
                component:  ForgotPassword
            }
        ]
    },
    {
        path: 'chat',
        component: MainLayout,
        children: [
            {
                path: '',
                component: ChatLayout
            }
        ]
    },
    //wildcard routes
    {
        path: '**',
        redirectTo: 'sign-in'
    }
];

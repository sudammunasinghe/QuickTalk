import { Routes } from '@angular/router';
import { AuthLayout } from './layouts/auth-layout/auth-layout';
import { SignIn } from './features/auth/sign-in/sign-in';
import { SignUp } from './features/auth/sign-up/sign-up';
import { ForgotPassword } from './features/auth/forgot-password/forgot-password';
import { MainLayout } from './layouts/main-layout/main-layout';
import { ChatPage } from './features/chat/pages/chat-page/chat-page';
import { ResetPassword } from './features/auth/reset-password/reset-password';

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
            },
            {
                path: 'reset-password',
                component: ResetPassword
            }
        ]
    },
    {
        path: 'chat',
        component: MainLayout,
        children: [
            {
                path: '',
                component: ChatPage
            }
        ]
    },
    //wildcard routes
    {
        path: '**',
        redirectTo: 'sign-in'
    }
];

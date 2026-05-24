import { Routes } from '@angular/router';
import { AuthLayout } from './layouts/auth-layout/auth-layout';
import { SignIn } from './features/auth/sign-in/sign-in';
import { SignUp } from './features/auth/sign-up/sign-up';
import { ForgotPassword } from './features/auth/forgot-password/forgot-password';
import { MainLayout } from './layouts/main-layout/main-layout';

export const routes: Routes = [
    {
        path: '',
        component: AuthLayout,
        children: [
            {
                path: '',
                redirectTo: 'sign-in',
                pathMatch: 'full'
            },
            {
                path: 'sign-in',
                component: SignIn
            },
            {
                path: 'sign-up',
                component: SignUp
            },
            {
                path: 'forgot-password',
                component:  ForgotPassword
            }
        ]
    },
    {
        path: 'app',
        component: MainLayout,
        children: [
            //feature routes
            //chat
            //profile
        ]
    },
    //wildcard routes
    {
        path: '**',
        redirectTo: 'sign-in'
    }
];

import { Routes } from '@angular/router';
import { LayoutsComponent } from './components/layouts/layouts.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { authGuard } from './guard/auth.guard';
import { RegisterComponent } from './components/register/register.component';

export const routes: Routes = [
    {
        path: "login",
        component: LoginComponent
    },
    {
        path: "register",
        component: RegisterComponent
    },
    {
        path: "",
        component: LayoutsComponent,
        canActivateChild: [authGuard],
        children: [
            {
                path: "",
                component: HomeComponent
            }
        ]
    }
];

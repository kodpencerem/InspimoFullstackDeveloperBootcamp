import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
    {
        path: "login",
        loadComponent: ()=> import("./components/login/login.component")
    },
    {
        path: "register",
        loadComponent: ()=> import("./components/register/register.component")
    },
    {
        path: "",
        loadComponent: ()=> import("./components/layouts/layouts.component"),
        canActivateChild: [authGuard],
        children: [
            {
                path: "",
                loadComponent: ()=> import("./components/home/home.component")
            },
            {
                path: "chats",
                loadComponent: ()=> import("./components/chats/chats.component")
            },
            {
                path: "chats/:id",
                loadComponent: ()=> import("./components/chats/chats.component")
            }
        ]
    },
    {
        path: "**",
        loadComponent: ()=> import("./components/not-found/not-found.component")
    }
];

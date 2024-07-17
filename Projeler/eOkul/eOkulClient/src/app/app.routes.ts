import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: "login",
        loadComponent: () => import("./components/login/login.component")
    },
    {
        path: "",
        loadComponent: ()=> import("./components/landspaces/landspaces.component")
    },
    {
        path: "admin",
        loadComponent: ()=> import("./components/layouts/layouts.component"),
        children: [
            {
                path: "",
                loadComponent: () => import("./components/home/home.component")
            },
            {
                path: "user-types",
                loadComponent: () => import("./components/user-types/user-types.component")
            }
        ]
    },
    {
        path: "**",
        loadComponent: () => import("./components/not-found/not-found.component")
    }
];

import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { ContactComponent } from './contact/contact.component';
import { canDeactivateGuard } from '../candeactivate.guard';
import { LayoutsComponent } from './layouts/layouts.component';
import { LoginComponent } from './login/login.component';
import { canactivatechildGuard } from '../canactivatechild.guard';
import { canactivateGuard } from '../canactivate.guard';
import { inject } from '@angular/core';
import { AuthService } from './auth.service';

export const routes: Routes = [
    {
        path: "login",
        component: LoginComponent
    },
    {
        path: "",
        component: LayoutsComponent,
        //canActivateChild: [canactivatechildGuard],
        canActivateChild: [()=> inject(AuthService).isAuthenticated()], //functional guard
        children: [
            {
                path: "home",
                component: HomeComponent,
                canDeactivate: [canDeactivateGuard]
            },
            {
                path:"about",
                component: AboutComponent, 
                canActivate: [canactivateGuard]               
            },
            {
                path: "contact",
                component: ContactComponent
            }
        ]
    }
    
];

import { Routes } from '@angular/router';
import  UILayoutsComponent  from './ui/layouts/layouts.component';
import  AdminLayoutsComponent  from './admin/layouts/layouts.component';
import UIHomeComponent from './ui/home/home.component';
import AdminHomeComponent from './admin/home/home.component';

export const routes: Routes = [
    {
        path: "",
        component: UILayoutsComponent,
        children: [
            {
                path: "",
                component: UIHomeComponent
            }
        ]
    },
    {
        path: "ui",
        component: UILayoutsComponent,
        children: [
            {
                path: "",
                component: UIHomeComponent
            }
        ]
    },
    {
        path: "admin",
        component: AdminLayoutsComponent,
        children: [
            {
                path: "",
                component: AdminHomeComponent
            }
        ]
    },
];

import { Routes } from '@angular/router';
import { LayoutComponent } from './components/layout/layout.component';
import { HomeComponent } from './components/home/home.component';
import { AboutMeComponent } from './components/about-me/about-me.component';
import { ContactComponent } from './components/contact/contact.component';
import { BlogsComponent } from './components/blogs/blogs.component';

export const routes: Routes = [   
    {
        path: "",
        component: LayoutComponent,
        children: [
            {
                path: "",
                component: HomeComponent
            },
            {
                path: "about-me",
                component: AboutMeComponent
            },
            {
                path: "contact",
                component: ContactComponent
            },
            {
                path: "blogs",
                component: BlogsComponent
            }
        ]
    }
];

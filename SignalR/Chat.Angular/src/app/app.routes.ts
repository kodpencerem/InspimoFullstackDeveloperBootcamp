import { Routes } from '@angular/router';
import { ChatsComponent } from './chats/chats.component';
import { GroupsComponent } from './groups/groups.component';

export const routes: Routes = [
    {
        path: "",
        component: ChatsComponent
    },
    {
        path: "groups/:name",
        component: GroupsComponent
    }
];

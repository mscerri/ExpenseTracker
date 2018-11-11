import { ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';

import { RootComponent } from './root/root.component';
import { MyExpensesComponent } from './my-expenses/my-expenses.component';
import { MyProfileComponent } from './my-profile/my-profile.component';

import { AuthGuard } from '../auth.guard';

export const Routing: ModuleWithProviders = RouterModule.forChild([
    {
        path: 'dashboard',
        component: RootComponent, canActivate: [AuthGuard],

        children: [
            { path: '', component: MyExpensesComponent },
            { path: 'expenses', component: MyExpensesComponent },
            { path: 'profile', component: MyProfileComponent },
        ]
    }
]);
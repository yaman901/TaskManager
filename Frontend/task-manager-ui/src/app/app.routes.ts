import { Routes } from '@angular/router';
import { Tasks } from './pages/tasks/tasks';
import { UserList } from './pages/users/users';
import { Projects } from './pages/projects/projects';

export const routes: Routes = [
  { path: 'tasks', component: Tasks },
  { path: 'users', component: UserList },
  { path: 'projects', component: Projects },
  { path: '', redirectTo: 'tasks', pathMatch: 'full' }
];

import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TasksComponent } from './tasks/tasks.component';
import { CreateTaskComponent } from './create-task/create-task.component';
import { UpdateTaskComponent } from './update-task/update-task.component';

export const routes: Routes = 
[
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full'
      },
      {
        path: 'home',
        component: HomeComponent,
        title: 'Home'
      },
      {
        path: 'tasks',
        component: TasksComponent,
        title: 'Tasks'
      },
      {
        path: 'create-task',
        component: CreateTaskComponent,
        title: 'Create Task'
      },
      {
        path: 'update-task/:id',
        component: UpdateTaskComponent,
        title: 'Update Task'
      },
];

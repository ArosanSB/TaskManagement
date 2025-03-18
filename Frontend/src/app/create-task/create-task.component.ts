import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { RouterModule } from '@angular/router';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { CommonModule } from '@angular/common'; 
import { FormsModule } from '@angular/forms';
import { CreateTaskRequest, TaskItemDto, TasksService } from '../api-client';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-task',
  providers: [provideNativeDateAdapter()],
  imports: [
    MatCardModule, MatButtonModule, RouterModule, MatFormFieldModule, 
    MatInputModule, MatDatepickerModule, MatSlideToggleModule, 
    CommonModule, FormsModule
  ],
  templateUrl: './create-task.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  styleUrl: './create-task.component.css'
})
export class CreateTaskComponent {
  taskTitle: string = '';
  taskDescription: string = '';
  taskDueDate: string | null = null;
  isCompleted: boolean = false;

  constructor(private tasksService: TasksService, private router: Router) {}

  toggleCompletion(): void {
    this.isCompleted = !this.isCompleted;
  }

  createTask(): void {
    const newTask: CreateTaskRequest = {
      title: this.taskTitle,
      description: this.taskDescription,
      dueDate: this.taskDueDate ? new Date(this.taskDueDate).toISOString() : '',
      isCompleted: this.isCompleted
    };

    this.tasksService.createtaskPost(newTask).subscribe({
      next: () => {
        console.log('Task Created:', newTask);
        alert('Task successfully created!');
        this.router.navigate(['/tasks']);
      },
      error: (error) => console.error('Error creating task:', error)
    });
  }
}

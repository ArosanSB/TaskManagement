import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { CommonModule } from '@angular/common'; 
import { FormsModule } from '@angular/forms';
import { TaskItemDto, TasksService } from '../api-client';
import { Router } from '@angular/router';

@Component({
  selector: 'app-update-task',
  providers: [provideNativeDateAdapter()],
  imports: [ MatCardModule, MatButtonModule, RouterModule, MatFormFieldModule, MatInputModule, MatDatepickerModule, MatSlideToggleModule, CommonModule, FormsModule ],
  templateUrl: './update-task.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush, 
  styleUrl: './update-task.component.css'
})
export class UpdateTaskComponent implements OnInit {
  taskID?: string = '';
  taskTitle?: string | null = '';
  taskDescription?: string | null = '';
  taskDueDate?: string | null = null;
  isCompleted?: boolean = false;

  constructor(
    private tasksService: TasksService, 
    private router: Router, 
    private route: ActivatedRoute,
    private cdRef: ChangeDetectorRef 
  ) {}

  ngOnInit(): void {
    this.taskID = this.route.snapshot.paramMap.get('id') || undefined;
    if (this.taskID) {
      this.fetchTask(this.taskID);
    }
  }

  fetchTask(id: string): void {
    this.tasksService.getTaskByIDIdGet(id).subscribe({
      next: (task) => {
        console.log("Fetched Task: ", task);
        this.taskID = task.id;
        this.taskTitle = task.title;
        this.taskDescription = task.description;
        this.taskDueDate = task.dueDate;
        this.isCompleted = task.isCompleted;

        this.cdRef.detectChanges(); 
      },
      error: (error) => console.error('Error fetching task:', error)
    });
  }

  toggleCompletion(): void {
    this.isCompleted = !this.isCompleted;
  }

  checkIfFormValid(): boolean {
    return this.taskTitle !== null && 
    this.taskTitle !== undefined && 
    this.taskTitle.length > 0 && 
    this.taskDescription !== null &&
    this.taskDescription !== undefined &&
    this.taskDescription.length > 0 && 
    this.taskDueDate !== null &&
    this.taskDueDate !== undefined &&
    this.taskDueDate.length > 0 ? true : false;
  }

  updateTask(): void {
    if (!this.checkIfFormValid()) {
      alert('Please fill out all fields before submitting.');
      return;
    }
    const updatedTask: TaskItemDto = {
      id: this.taskID,
      title: this.taskTitle,
      description: this.taskDescription,
      dueDate: this.taskDueDate ? new Date(this.taskDueDate).toISOString() : '',
      isCompleted: this.isCompleted
    };

    this.tasksService.updatetaskPut(updatedTask).subscribe({
      next: () => {
        console.log('Task Updated:', updatedTask);
        alert('Task successfully updated!');
        this.router.navigate(['/tasks']);
      },
      error: (error) => console.error('Error updating task:', error)
    });
  }

  deleteTask(): void {
    this.tasksService.deletetaskIdDelete(this.taskID!).subscribe({
      next: () => {
        console.log('Task Deleted:', this.taskID);
        alert('Task successfully deleted!');
        this.router.navigate(['/tasks']);
      },
      error: (error) => console.error('Error deleting task:', error)
    });
  }
}

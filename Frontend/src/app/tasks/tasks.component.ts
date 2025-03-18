import { MatListModule} from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';
import { TaskItemDto } from '../api-client';
import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatSort, MatSortModule} from '@angular/material/sort';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { TasksService } from '../api-client';
import { RouterModule } from '@angular/router';
import { MatButton } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule} from '@angular/material/checkbox';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tasks',
  imports: [MatListModule, MatCardModule, MatFormFieldModule, MatInputModule, MatTableModule, MatSortModule, MatPaginatorModule, RouterModule, MatButton, MatIconModule, MatCheckboxModule],
  templateUrl: './tasks.component.html',
  styleUrl: './tasks.component.css'
})
export class TasksComponent implements AfterViewInit {
  displayedColumns: string[] = ['title', 'description', 'dueDate', 'isCompleted'];
  dataSource: MatTableDataSource<TaskItemDto> = new MatTableDataSource();
  private task: TasksService;

  constructor(private tasksService: TasksService, private router: Router) {
    this.task = tasksService;
    this.fetchTasks();
  }

  @ViewChild(MatPaginator)
  paginator: MatPaginator = new MatPaginator;
  @ViewChild(MatSort)
  sort: MatSort = new MatSort;
  tasks: TaskItemDto[] = [];
  

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  fetchTasks(): void {
    this.tasksService.getallTasksGet().subscribe({
      next: (response) => {
        console.log("Tasks Received:", response); // Debugging log
        this.tasks = response; // Assign API data to tasks array
        this.dataSource = new MatTableDataSource(this.tasks);

      },
      error: (error) => {
        console.error("Error fetching tasks:", error);
      }
    });
  }

  RunIsCompleted(task: TaskItemDto) {
    console.log('Task is completed: ', task);
  }

  RunEditTask(task: TaskItemDto) {
    console.log('Edit task: ', task);
    this.router.navigate(['/update-task', task.id]);
  }

}



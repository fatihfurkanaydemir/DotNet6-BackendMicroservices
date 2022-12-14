import { Component, OnInit } from '@angular/core';
import { IWorkflow } from 'src/app/models/IWorkflow';
import { WorkflowService } from '../services/workflow-service.service';

@Component({
  selector: 'app-workflow-table',
  templateUrl: './workflow-table.component.html',
  styleUrls: ['./workflow-table.component.scss'],
})
export class WorkflowTableComponent implements OnInit {
  workflows: IWorkflow[] = [];

  pageNumber: number = 1;
  pageSize: number = 12;
  dataCount: number = this.workflows.length;

  constructor(private workflowService: WorkflowService) {}

  ngOnInit(): void {
    this.getWorkflows();
  }

  getWorkflows() {
    this.workflowService
      .getAllWorkflows(this.pageNumber, this.pageSize)
      .subscribe({
        next: (response) => {
          this.workflows = response.data;
          this.dataCount = +response.dataCount;
        },
        error: (err) => {
          console.log(err);
        },
      });
  }

  onRemove(workflow: IWorkflow) {
    this.workflowService.deleteWorkflow(workflow.id).subscribe({
      next: (response) => {
        this.getWorkflows();
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  onPageChange(newPageNumber: number) {
    this.pageNumber = newPageNumber;
    this.getWorkflows();
  }
}

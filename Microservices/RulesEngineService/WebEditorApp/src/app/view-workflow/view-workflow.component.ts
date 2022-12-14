import { Component, OnInit } from '@angular/core';
import { IRule } from 'src/app/models/IRule';
import { IWorkflow } from 'src/app/models/IWorkflow';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { WorkflowService } from '../services/workflow-service.service';

@Component({
  selector: 'app-view-workflow',
  templateUrl: './view-workflow.component.html',
  styleUrls: ['./view-workflow.component.scss'],
})
export class ViewWorkflowComponent implements OnInit {
  workflow: IWorkflow = {
    id: '',
    workflowName: '',
    rules: [],
  };

  tempRules = this.workflow.rules;

  newRule: IRule = {
    ruleName: '',
    errorMessage: '',
    expression: '',
    successEvent: '',
  };

  pageNumber: number = 1;
  pageSize: number = 12;
  dataCount: number = 0;

  constructor(
    private route: ActivatedRoute,
    private workflowService: WorkflowService
  ) {}

  ngOnInit(): void {
    this.getWorkflow();
  }

  getWorkflow() {
    this.workflowService
      .getWorkflow(this.route.snapshot.params['workflowId'])
      .subscribe({
        next: (response) => {
          this.workflow = response.data;
          this.dataCount = this.workflow.rules.length;
          this.tempRules = this.workflow.rules.slice(
            (this.pageNumber - 1) * this.pageSize,
            this.pageNumber * this.pageSize
          );
        },
        error: (err) => {
          console.log(err);
        },
      });
  }

  onPageChange(newPageNumber: number) {
    this.pageNumber = newPageNumber;
    this.tempRules = this.workflow.rules.slice(
      (this.pageNumber - 1) * this.pageSize,
      this.pageNumber * this.pageSize
    );
  }

  onAdd(form: NgForm) {
    if (form.invalid) return;

    this.workflowService.addRule(this.workflow.id, this.newRule).subscribe({
      next: (response) => {
        this.getWorkflow();
        this.newRule = {
          ruleName: '',
          errorMessage: '',
          expression: '',
          successEvent: '',
        };
        form.reset();
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  onRemove(rule: IRule) {
    this.workflowService.deleteRule(this.workflow.id, rule).subscribe({
      next: (response) => {
        this.getWorkflow();
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  onSave(rule: IRule, form: NgForm) {
    if (form.invalid) return;

    this.workflowService.updateRule(this.workflow.id, rule).subscribe({
      next: (response) => {
        this.getWorkflow();
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}

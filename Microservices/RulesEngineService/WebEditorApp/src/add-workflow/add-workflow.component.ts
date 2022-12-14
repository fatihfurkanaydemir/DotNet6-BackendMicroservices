import { Component, EventEmitter, OnInit, Output } from '@angular/core';

import { NgForm } from '@angular/forms';
import {
  NgbModal,
  ModalDismissReasons,
  NgbActiveModal,
} from '@ng-bootstrap/ng-bootstrap';
import { IWorkflow } from 'src/app/models/IWorkflow';
import { WorkflowService } from 'src/app/services/workflow-service.service';
// import { ICategoryCreate } from 'src/app/models/ICategoryCreate';

// import { ToastService } from 'src/app/services/toast.service';
// import { CategoriesService } from '../../../services/categories.service';

@Component({
  selector: 'app-add-workflow',
  templateUrl: './add-workflow.component.html',
  styleUrls: ['./add-workflow.component.css'],
})
export class AddWorkflowComponent implements OnInit {
  @Output('OnWorkflowAdded') workflowAddedEvent: EventEmitter<boolean> =
    new EventEmitter();

  closeResult: string = '';

  workflow: IWorkflow = {
    id: '',
    workflowName: '',
    rules: [],
  };

  constructor(
    private workflowService: WorkflowService,
    private modalService: NgbModal
  ) {}

  onSubmit(addWorkflowForm: NgForm, modal: NgbActiveModal) {
    if (addWorkflowForm.invalid) return;

    this.workflowService.createWorkflow(this.workflow).subscribe({
      next: (response) => {
        modal.dismiss();
        addWorkflowForm.reset();
        this.workflowAddedEvent.emit(true);
      },
      error: (err) => {
        console.log(err);
        modal.dismiss();
        addWorkflowForm.reset();
        this.workflowAddedEvent.emit(true);
      },
    });
  }

  ngOnInit() {}

  open(content: any) {
    this.modalService
      .open(content, { ariaLabelledBy: 'modal-basic-title' })
      .result.then(
        (result) => {
          this.closeResult = `Closed with: ${result}`;
        },
        (reason) => {
          this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
        }
      );
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
}

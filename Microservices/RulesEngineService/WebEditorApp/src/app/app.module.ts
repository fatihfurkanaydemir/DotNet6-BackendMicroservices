import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AddWorkflowComponent } from 'src/add-workflow/add-workflow.component';
import { WorkflowTableComponent } from './workflow-table/workflow-table.component';
import { ViewWorkflowComponent } from './view-workflow/view-workflow.component';

@NgModule({
  declarations: [
    AppComponent,
    AddWorkflowComponent,
    WorkflowTableComponent,
    ViewWorkflowComponent,
  ],
  imports: [
    BrowserModule,
    NgbModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}

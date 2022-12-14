import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IWorkflow } from 'src/app/models/IWorkflow';
import { IApiResponse } from '../models/IApiResponse';
import { IApiResponseSingle } from '../models/IApiResponseSingle';
import { IRule } from '../models/IRule';

@Injectable({
  providedIn: 'root',
})
export class WorkflowService {
  url = environment.apiUrl + '/Workflow';

  constructor(private httpClient: HttpClient) {}

  getAllWorkflows(
    pageNumber: number,
    pageSize: number
  ): Observable<IApiResponse> {
    return this.httpClient.get<IApiResponse>(
      `${this.url}?PageNumber=${pageNumber}&PageSize=${pageSize}`
    );
  }

  getWorkflow(id: string): Observable<IApiResponse> {
    return this.httpClient.get<IApiResponse>(`${this.url}/${id}`);
  }

  createWorkflow(workflow: IWorkflow): Observable<IApiResponseSingle> {
    return this.httpClient.post<IApiResponseSingle>(this.url, workflow);
  }

  deleteWorkflow(id: string): Observable<IApiResponseSingle> {
    return this.httpClient.delete<IApiResponseSingle>(`${this.url}/${id}`);
  }

  addRule(id: string, rule: IRule): Observable<IApiResponseSingle> {
    return this.httpClient.post<IApiResponseSingle>(
      `${this.url}/${id}/Rule`,
      rule
    );
  }

  updateRule(id: string, rule: IRule): Observable<IApiResponseSingle> {
    return this.httpClient.put<IApiResponseSingle>(
      `${this.url}/${id}/Rule`,
      rule
    );
  }

  deleteRule(id: string, rule: IRule): Observable<IApiResponseSingle> {
    return this.httpClient.delete<IApiResponseSingle>(
      `${this.url}/${id}/Rule/${rule.ruleName}`
    );
  }
}

import { IRule } from './IRule';

export interface IWorkflow {
  id: string;
  workflowName: string;
  rules: IRule[];
}

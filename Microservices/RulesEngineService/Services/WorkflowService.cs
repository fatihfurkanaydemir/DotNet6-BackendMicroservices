using RulesEngineService.DTOs;
using RulesEngineService.Entities;
using RulesEngineService.Exceptions;
using RulesEngineService.Wrappers;

namespace RulesEngineService.Services;

public class WorkflowService
{
  private WorkflowCollectionService _workflowCollectionService;
  public WorkflowService(WorkflowCollectionService workflowCollectionService)
  {
    _workflowCollectionService = workflowCollectionService;
  }

  public async Task<Response<string>> CreateWorkflow(WorkflowDTO workflow)
  {
    var result = await _workflowCollectionService.AddAsync(new Workflow
    {
      WorkflowName = workflow.WorkflowName,
      Rules = new List<RulesEngine.Models.Rule>()
    });

    return new Response<string>(result, message: "Workflow created");
  }

  public async Task<PagedResponse<IEnumerable<Workflow>>> GetAllWorkflows(int pageNumber, int pageSize)
  {
    var workflows = await _workflowCollectionService.GetAllPagedAsync(pageNumber, pageSize);
    var dataCount = await _workflowCollectionService.GetWorkflowCountAsync();

    return new PagedResponse<IEnumerable<Workflow>>(workflows, pageNumber, pageSize, dataCount);
  } 

  public async Task<Response<Workflow>> GetWorkflowById(string id)
  {
    var result = await _workflowCollectionService.GetById(id);
    return new Response<Workflow>(result, message: "");
  }

  public async Task AddRule(string id, RuleDTO ruleDTO)
  {
    var workflow = await _workflowCollectionService.GetById(id);
    if (workflow == null) throw new ApiException("Workflow not found.");

    var rules = workflow.Rules as List<RulesEngine.Models.Rule>;
    var rule = rules.SingleOrDefault(r => r.RuleName == ruleDTO.RuleName);
    if (rule != null) throw new ApiException("Rule already exists.");

    rules.Add(new RulesEngine.Models.Rule()
    {
      RuleName = ruleDTO.RuleName,
      ErrorMessage = ruleDTO.ErrorMessage,
      Expression = ruleDTO.Expression,
      SuccessEvent = ruleDTO.SuccessEvent,
    });

    workflow.Rules = rules;
    await _workflowCollectionService.UpdateAsync(workflow);
  }

  public async Task UpdateRule(string id, RuleDTO ruleDTO)
  {
    var workflow = await _workflowCollectionService.GetById(id);
    if (workflow == null) throw new ApiException("Workflow not found.");

    var rules = workflow.Rules as List<RulesEngine.Models.Rule>;
    var rule = rules.SingleOrDefault(r => r.RuleName == ruleDTO.RuleName);
    if (rule == null) throw new ApiException("Rule not found.");

    rule.RuleName = ruleDTO.RuleName;
    rule.ErrorMessage = ruleDTO.ErrorMessage;
    rule.Expression = ruleDTO.Expression;
    rule.SuccessEvent = ruleDTO.SuccessEvent;
    
    workflow.Rules = rules;
    await _workflowCollectionService.UpdateAsync(workflow);
  }

  public async Task DeleteRule(string id, string ruleName)
  {
    var workflow = await _workflowCollectionService.GetById(id);
    if (workflow == null) throw new ApiException("Workflow not found.");

    var rules = workflow.Rules as List<RulesEngine.Models.Rule>;
    var rule = rules.SingleOrDefault(r => r.RuleName == ruleName);
    if (rule == null) throw new ApiException("Rule not found.");

    rules.Remove(rule);

    workflow.Rules = rules;
    await _workflowCollectionService.UpdateAsync(workflow);
  }

  public async Task DeleteWorkflow(string id)
  {
    var workflow = await _workflowCollectionService.GetById(id);
    if (workflow == null) throw new ApiException("Workflow not found.");

    await _workflowCollectionService.RemoveAsync(id);
  }
}

namespace RulesEngineService.Controllers;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RulesEngineService.DTOs;
using RulesEngineService.Entities;
using RulesEngineService.Parameters;
using RulesEngineService.Services;

public class WorkflowController : BaseApiController
{
  WorkflowService _workflowService;
  public WorkflowController(WorkflowService workflowService)
  {
    _workflowService = workflowService;
  }

  // POST api/<controller>
  [HttpGet("{id}")]
  public async Task<IActionResult> Get(string id)
  {
    return Ok(await _workflowService.GetWorkflowById(id));
  }

  // POST api/<controller>
  [HttpGet]
  public async Task<IActionResult> GetAll([FromQuery] RequestParameter parameter)
  {
    return Ok(await _workflowService.GetAllWorkflows(parameter.PageNumber, parameter.PageSize));
  }


  // POST api/<controller>
  [HttpPost]
  public async Task<IActionResult> Post(WorkflowDTO workflowDTO)
  {
    return Ok(await _workflowService.CreateWorkflow(workflowDTO));
  }

  // DELETE api/<controller>
  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(string id)
  {
    await _workflowService.DeleteWorkflow(id);
    return Ok();
  }

  // POST api/<controller>/:id/Rule
  [HttpPost("{id}/Rule")]
  public async Task<IActionResult> AddRule(string id, RuleDTO ruleDTO)
  {
    await _workflowService.AddRule(id, ruleDTO);
    return Ok();
  }

  // PUT: api/<controller>
  [HttpPut("{id}/Rule")]
  public async Task<IActionResult> UpdateRule(string id, RuleDTO ruleDTO)
  {
    await _workflowService.UpdateRule(id, ruleDTO);
    return Ok();
  }

  // DELETE: api/<controller>
  [HttpDelete("{id}/Rule/{ruleName}")]
  public async Task<IActionResult> DeleteRule(string id, string ruleName)
  {
    await _workflowService.DeleteRule(id, ruleName);
    return Ok();
  }
}

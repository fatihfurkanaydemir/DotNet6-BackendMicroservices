namespace RulesEngineService.DTOs;

public class WorkflowDTO
{
  public string WorkflowName { get; set; }
  public List<RuleDTO> Rules { get; set; }
}

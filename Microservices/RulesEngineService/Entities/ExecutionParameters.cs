using System.Dynamic;

namespace RulesEngineService.Entities;

public class ExecutionParameters
{
  public string WorkflowName { get; set; }
  public ExpandoObject Input { get; set; }
}

namespace RulesEngineService.DTOs;

public class RuleDTO
{
  public string RuleName { get; set; }
  public string SuccessEvent { get; set; }
  public string ErrorMessage { get; set; }
  public string Expression { get; set; }
}

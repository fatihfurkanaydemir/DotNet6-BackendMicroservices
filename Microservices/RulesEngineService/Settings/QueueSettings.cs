namespace RulesEngineService.Settings;

public class QueueSettings
{
  public string HostName { get; set; } = default!;
  public string UserName { get; set; } = default!;
  public string Password { get; set; } = default!;
  public string ReceiveQueueName { get; set; } = default!;
  public string SendQueueName { get; set; } = default!;
}

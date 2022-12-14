namespace RulesEngineService.Settings;

public class MongoDBSettings
{
  public string ConnectionString { get; set; } = default!;
  public string DatabaseName { get; set; } = default!;
  public string WorkflowCollectionName { get; set; } = default!;
}

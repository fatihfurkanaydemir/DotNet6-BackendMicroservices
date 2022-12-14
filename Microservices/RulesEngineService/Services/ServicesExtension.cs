namespace RulesEngineService.Services;

public static class ServicesExtension
{
  public static void RegisterServices(this IServiceCollection services)
  {
    services.AddSingleton<WorkflowCollectionService>();
    services.AddScoped<WorkflowService>();
  }
}

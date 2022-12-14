using Microsoft.AspNetCore.SpaServices.AngularCli;
using RulesEngineService;
using RulesEngineService.Middlewares;
using RulesEngineService.Services;
using RulesEngineService.Settings;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json")
  .Build();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSpaStaticFiles(configuration =>
{
  configuration.RootPath = "WebEditorApp/dist";
});

builder.Services.Configure<MongoDBSettings>(config.GetSection("MongoDBSettings"));
builder.Services.Configure<QueueSettings>(config.GetSection("QueueSettings"));
builder.Services.RegisterServices();

var app = builder.Build();

app.MapControllers();

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
  endpoints.MapControllers();
});

app.UseStaticFiles();

if (!app.Environment.IsDevelopment())
{
    app.UseSpaStaticFiles();
}


app.UseSpa(spa =>
{
    spa.Options.SourcePath = "WebEditorApp";

    if (app.Environment.IsDevelopment())
    {
        spa.UseAngularCliServer(npmScript: "start");
    }
});

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "Rules Engine API");
    c.RoutePrefix = "api";
  });
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.ConfigureRulesEngine(app.Configuration.GetSection("QueueSettings").Get<QueueSettings>());

app.UseHttpsRedirection();

app.Run();

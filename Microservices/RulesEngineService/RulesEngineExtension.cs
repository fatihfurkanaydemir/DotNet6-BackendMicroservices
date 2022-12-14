namespace RulesEngineService;

using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RulesEngine.Models;
using RulesEngineService.Entities;
using RulesEngineService.Services;
using RulesEngineService.Settings;
using System.Dynamic;
using System.Text;

public static class RulesEngineExtension
{
  public static void ConfigureRulesEngine(this IApplicationBuilder app, QueueSettings queueSettings)
  {
    var workflowCollectionService = app.ApplicationServices.GetRequiredService<WorkflowCollectionService>();

    ConfigureQueues(workflowCollectionService, queueSettings);
  }

  private static void ConfigureQueues(WorkflowCollectionService workflowCollectionService, QueueSettings queueSettings)
  {
    var factory = new ConnectionFactory() 
    { 
      HostName = queueSettings.HostName,
      UserName = queueSettings.UserName,
      Password = queueSettings.Password,
      DispatchConsumersAsync = true
    };

    var connection = factory.CreateConnection();
    var channel = connection.CreateModel();
    
    channel.QueueDeclare(queue: queueSettings.ReceiveQueueName,
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    channel.QueueDeclare(queue: queueSettings.SendQueueName,
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    var consumer = new AsyncEventingBasicConsumer(channel);
    consumer.Received += async (model, ea) =>
    {
      var recv_body = ea.Body.ToArray();
      var recv_str = Encoding.UTF8.GetString(recv_body);
      var executionParams = JsonConvert.DeserializeObject<ExecutionParameters>(recv_str)!;
      

      var workflow = await workflowCollectionService.GetByWorkflowNameAsync(executionParams.WorkflowName);

      var engine = new RulesEngine.RulesEngine(new RulesEngineService.Entities.Workflow[] { workflow });
      List<RuleResultTree> resultList = await engine.ExecuteAllRulesAsync(executionParams.WorkflowName, executionParams.Input);

      string send_str = JsonConvert.SerializeObject(resultList);
      var send_body = Encoding.UTF8.GetBytes(send_str);

      channel.BasicPublish(exchange: "",
                           routingKey: queueSettings.SendQueueName,
                           basicProperties: null,
                           body: send_body);
    };

    channel.BasicConsume(queue: queueSettings.ReceiveQueueName,
                         autoAck: true,
                         consumer: consumer);
    
  }
}

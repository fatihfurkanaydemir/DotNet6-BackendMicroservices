using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RulesEngineService.Entities;
using RulesEngineService.Settings;

namespace RulesEngineService.Services;

public class WorkflowCollectionService
{
  private readonly IMongoCollection<Workflow> _collection;

  public WorkflowCollectionService(IOptions<MongoDBSettings> options)
  {
    MongoDBSettings dbSettings = options.Value;
    var mongoClient = new MongoClient(dbSettings.ConnectionString);

    var mongoDatabase = mongoClient.GetDatabase(dbSettings.DatabaseName);

    _collection = mongoDatabase.GetCollection<Workflow>(dbSettings.WorkflowCollectionName);
  }

  public async Task<List<Workflow>> GetAllPagedAsync(int pageNumber, int pageSize)
  {
    return await _collection
      .Find(_ => true)
      .Skip((pageNumber - 1) * pageSize)
      .Limit(pageSize)
      .ToListAsync();
  }

  public async Task<int> GetWorkflowCountAsync()
  {
    return (int)(await _collection.Find(_ => true).CountDocumentsAsync());
  }

  public async Task<Workflow> GetById(string id)
  {
    return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
  }

  public async Task<Workflow> GetByWorkflowNameAsync(string workflowName)
  {
    return  await _collection.Find(x => x.WorkflowName == workflowName).FirstOrDefaultAsync();
  }

  public async Task<string> AddAsync(Workflow workflow)
  {
    await _collection.InsertOneAsync(workflow);
    return workflow.Id;
  }

  public async Task<string> UpdateAsync(Workflow workflow)
  {
    await _collection.ReplaceOneAsync(x => x.Id == workflow.Id, workflow);
    return workflow.Id;
  }

  public async Task RemoveAsync(string id)
  {
      await _collection.DeleteOneAsync(x => x.Id == id);
  }
}

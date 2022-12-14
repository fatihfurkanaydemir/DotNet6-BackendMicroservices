using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RulesEngineService.Entities;

public class Workflow: RulesEngine.Models.Workflow
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]

  public string Id { get; set; }
}

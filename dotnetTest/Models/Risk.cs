using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace dotnetTest.Models;

public class Risk
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string? RiskId { get; set; }
    
    public string? ProjectId { get; set; }
    
    public string? RiskDescription  { get; set; }
    
    public int? RiskStatus { get; set; }
}
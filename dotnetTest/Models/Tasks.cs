using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dotnetTest.Models;

public class Tasks
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string? TaskId { get; set; }
    
    public string? ProjectId { get; set; }
    
    public string? TaskDescription  { get; set; }
    
    public string? TaskDueDate  { get; set; }
    
    public float? EstimatedHours { get; set; }
    
    public bool? TaskActive { get; set; }
    
    public Member? AssignedTo { get; set; }
    

}
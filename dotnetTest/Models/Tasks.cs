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
    
    [Required]
    public string TaskDescription  { get; set; }
    
    [Required]
    public string TaskDueDate  { get; set; }
    
    [Required]
    public bool TaskActive { get; set; }

}